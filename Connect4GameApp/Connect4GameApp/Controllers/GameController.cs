using Microsoft.AspNetCore.Mvc;
using Connect4GameApp.Data;
using Connect4GameApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using Connect4GameApp.Client.State;
using Microsoft.AspNetCore.SignalR;
using Connect4GameApp.Hubs;

namespace Connect4GameApp.Controllers
{
    [ApiController]
    [Route("api/game/")]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public IActionResult GetGames()
        {
            var games = _context.Games?.ToList();
            return Ok(games);
        }

        [HttpGet("{code}")]
        public IActionResult GetGame(string code)
        {
            var game = _context.Games?.SingleOrDefault(g => g.Code == code);
            if (game == null) return NotFound("Game not found");
            return Ok(game);
        }

        [HttpPost("create")]
        public IActionResult CreateGame([FromBody] CreateGameRequest request)
        {
            var gameCode = GenerateRandomCode();
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized("User not authenticated");

            while (_context.Games != null && _context.Games.Any(g => g.Code == gameCode))
            {
                gameCode = GenerateRandomCode();
            }

            var game = new Game
            {
                Name = request.Name ?? "Connect4",
                Code = gameCode,
                GridSize = request.Grid ?? 7,
                HostId = userId,
                BoardColor = request.BoardColor ?? "#000000",
                Player1Color = request.Player1Color ?? "#FF0000",
                Player2Color = request.Player2Color ?? "#FFFF00"
            };

            if (_context != null)
            {
                _context.Games?.Add(game);
                _context.SaveChanges();
            }
            else
            {
                return StatusCode(500, "Database context is not available.");
            }


            return Ok(game);
        }



        [HttpPost("join")]
        public IActionResult JoinGame([FromBody] JoinGameRequest request)
        {
            var game = _context.Games?.SingleOrDefault(g => g.Code == request.GameCode || g.Id == request.GameId);
            if (game == null) return NotFound("Game not found");

            if (game.GuestId != null) return BadRequest("Game is already full");

            game.GuestId = request.GuestId;

            if (game.Status != GameStatus.AwaitingGuest)
            {
                game.Status = GameStatus.AwaitingHost;
            }

            _context.SaveChanges();

            return Ok(game);
        }

        [HttpPost("start")]
        public IActionResult StartGame([FromBody] JoinGameRequest request)
        {
            var game = _context.Games?.SingleOrDefault(g => g.Code == request.GameCode || g.Id == request.GameId);
            if (game == null) return NotFound("Game not found");

            if (game.GuestId == null) return BadRequest("Game is not full");

            game.Status = GameStatus.InProgress;
            _context.SaveChanges();

            return Ok(game);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MakeMove([FromBody] MakeMoveRequest request)
        {
            var game = await _context.Games?.SingleOrDefaultAsync(g => g.Code == request.GameCode || g.Id == request.GameId);
            if (game == null) return NotFound("Game not found");

            var playerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (playerId == null) return Unauthorized("User not authenticated");

            if (game.Status != GameStatus.InProgress) return BadRequest("Game is not in progress");

            var player = playerId == game.HostId ? 1 : playerId == game.GuestId ? 2 : 0;
            if (player == 0) return Unauthorized("Player is not part of this game");

            if (player == game.LastPlayer) return BadRequest("It's not your turn");

            List<int> grid;
            try
            {
                grid = game.GridState.Split(',').Select(int.Parse).ToList();
            }
            catch (FormatException)
            {
                // Initialize the grid with default values if the format is invalid
                grid = new List<int>(new int[42]);
                game.GridState = string.Join(",", grid);
            }

            try
            {
                var landingSpot = PlayPiece(grid, request.Column, player);
                game.GridState = string.Join(",", grid);
                game.LastPlayer = player;
                game.CurrentPlayerTurn = player == 1 ? 2 : 1;

                if (CheckForWin(grid) != GameState.WinState.No_Winner)
                {
                    game.Status = GameStatus.Finished;
                }
                await _context.SaveChangesAsync();

                var hubContext = HttpContext.RequestServices.GetService(typeof(IHubContext<GameHub>)) as IHubContext<GameHub>;
                if (hubContext == null) return StatusCode(500, "Hub context is not available.");
                await hubContext.Clients.Group(game.Code).SendAsync("ReceiveMove", request.Column, player);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(game);
        }
        private byte PlayPiece(List<int> grid, int column, int player)
        {
            if (CheckForWin(grid) != GameState.WinState.No_Winner) throw new ArgumentException("Game is over");

            if (grid[column] != 0) throw new ArgumentException("Column is full");

            var landingSpot = column;
            for (var i = column; i < 42; i += 7)
            {
                if (grid[landingSpot + 7] != 0) break;
                landingSpot = i;
            }

            grid[landingSpot] = player;

            return ConvertLandingSpotToRow(landingSpot);
        }

        private GameState.WinState CheckForWin(List<int> grid)
        {
            if (grid.Count(x => x != 0) < 7) return GameState.WinState.No_Winner;

            foreach (var scenario in GameState.WinningPlaces)
            {
                if (grid[scenario[0]] == 0) continue;

                if (grid[scenario[0]] == grid[scenario[1]] &&
                    grid[scenario[1]] == grid[scenario[2]] &&
                    grid[scenario[2]] == grid[scenario[3]])
                {
                    return (GameState.WinState)grid[scenario[0]];
                }
            }

            if (grid.Count(x => x != 0) == 42) return GameState.WinState.Tie;

            return GameState.WinState.No_Winner;
        }

        private byte ConvertLandingSpotToRow(int landingSpot)
        {
            return (byte)(Math.Floor(landingSpot / (decimal)7) + 1);
        }

        private bool CheckWinCondition(List<string> grid, int row, int column)
        {
            // Implement win condition checking logic here
            return false;
        }

        protected String GenerateRandomCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                code.Append(chars[random.Next(chars.Length)]);
            }
            return code.ToString();
        }
    }
}
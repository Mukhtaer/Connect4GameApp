using Connect4GameApp.Data;
using System.ComponentModel.DataAnnotations;

namespace Connect4GameApp.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [StringLength(10)]
        public required string Code { get; set; }

        public ApplicationUser? Host { get; set; }
        [Required]
        public required string HostId { get; set; }

        public ApplicationUser? Guest { get; set; }
        public string? GuestId { get; set; }

        [Required]
        public GameStatus Status { get; set; } = GameStatus.Created;

        [Required]
        public required string BoardColor { get; set; }

        [Required]
        public required string Player1Color { get; set; }

        [Required]
        public required string Player2Color { get; set; }

        [Required]
        public int GridSize { get; set; } = 7;

        public string GridState { get; set; } = string.Join(",", new string[42]);

        public int HostPoints { get; set; } = 0;
        public int GuestPoints { get; set; } = 0;
        public int CurrentPlayerTurn { get; set; } = 1; // 1 for Host, 2 for Guest
        public int LastPlayer { get; set; } = 0; // 0 for none, 1 for Host, 2 for Guest

        public void StartGame()
        {
            if (Guest != null && Host != null)
            {
                Status = GameStatus.InProgress;
            }
        }

        public void JoinGame(ApplicationUser guest)
        {
            if (Guest == null)
            {
                Guest = guest;
                Status = GameStatus.AwaitingHost;
                StartGame();
            }
        }

        public bool PlayTurn(ApplicationUser player, int column)
        {
            if (Status != GameStatus.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            if (player != Host && player != Guest)
                throw new UnauthorizedAccessException("Player is not part of this game.");

            var grid = GridState.Split(',').ToList();
            for (int row = 5; row >= 0; row--)
            {
                if (grid[row * 7 + column] == "")
                {
                    grid[row * 7 + column] = player == Host ? "H" : "G";
                    GridState = string.Join(",", grid);
                    if (CheckWinCondition(player))
                    {
                        Status = GameStatus.Finished;
                        if (player == Host)
                        {
                            HostPoints++;
                        }
                        else
                        {
                            GuestPoints++;
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        //host 

        private bool CheckWinCondition(ApplicationUser player)
        {
            // Implement win condition checking logic here
            // Return true if a player wins, otherwise false
            return false;
        }
    }
}
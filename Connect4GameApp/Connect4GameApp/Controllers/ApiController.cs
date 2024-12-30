using System.Security.Claims;
using Connect4GameApp.Data;
using Connect4GameApp.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ApiController : Controller {

       private readonly ApplicationDbContext _context;

    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<ApiController> _logger;

    public ApiController(ApplicationDbContext context, ILogger<ApiController> logger) {
        _context = context;
        _logger = logger;
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    //user 
    [HttpGet("user/{id}")]
    public IActionResult GetUser(string id) {
        try {
            var user = _context.Users.Find(id);
            if (user == null) {
                return NotFound("User not found");
            }
            return Ok(user);
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("user/logged-in")]
    public IActionResult GetLoggedInUser([FromBody] string userName) {
        try {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null) {
                return NotFound("User not found");
            }

            return Ok(user);
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

        [HttpGet("user/current")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Identity?.Name;

            if (userId == null || userName == null)
            {
                return Unauthorized();
            }

            var user = new
            {
                Id = userId,
                UserName = userName
            };

            return Ok(user);
        }
    


    //DEBUG API
    [HttpGet("debug/delete/all")]
    public IActionResult DeleteAll() {
        try{
           //users and games
            _context.Users.RemoveRange(_context.Users);
            _context.Games?.RemoveRange(_context.Games);
            _context.SaveChanges();
            return Ok("All users and games deleted");
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
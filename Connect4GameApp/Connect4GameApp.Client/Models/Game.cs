using System.ComponentModel.DataAnnotations;

namespace Connect4GameApp.Client.Models
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

        public Gamer? Host { get; set; }
        [Required]
        public required string HostId { get; set; }

        public Gamer? Guest { get; set; }
        public string? GuestId { get; set; }

        [Required]
        public GameStatus Status { get; set; } = GameStatus.Created;

        [Required]
        public required string BoardColor { get; set; }

        [Required]
        public required string Player1Color { get; set; }

        [Required]
        public required string Player2Color { get; set; }
        public int CurrentPlayerTurn { get; set; } = 1; // 1 for Host, 2 for Guest
        public int LastPlayer { get; set; } = 0; // 0 for none, 1 for Host, 2 for Guest

        [Required]
        public int GridSize { get; set; } = 7;

        public string GridState { get; set; } = string.Join(",", new string[42]);

        public int HostPoints { get; set; } = 0;
        public int GuestPoints { get; set; } = 0;
    }
}
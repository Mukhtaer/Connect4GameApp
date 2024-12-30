namespace Connect4GameApp.Client.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGameRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [StringLength(10, ErrorMessage = "Code cannot be longer than 10 characters.")]
        public string? Code { get; set; }
        public int? Grid { get; set; } = 7;

        [Required(ErrorMessage = "HostUsername is required.")]
        public string? HostUsername { get; set; }
        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "BoardColor must be a valid hex color code.")]
        public string? BoardColor { get; set; }

        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Player1Color must be a valid hex color code.")]
        public string? Player1Color { get; set; }

        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Player2Color must be a valid hex color code.")]
        public string? Player2Color { get; set; }
    }
}
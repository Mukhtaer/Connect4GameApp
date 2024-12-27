namespace Connect4GameApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGameRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [StringLength(10, ErrorMessage = "Code cannot be longer than 10 characters.")]
        public string? Code { get; set; }

        [Range(4, 20, ErrorMessage = "Grid size must be between 4 and 20.")]
        public int? Grid { get; set; }

        [Required(ErrorMessage = "HostUsername is required.")]
        public string? HostUsername { get; set; }
    }
}
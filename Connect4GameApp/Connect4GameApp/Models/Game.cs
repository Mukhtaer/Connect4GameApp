using Connect4GameApp.Data;

namespace Connect4GameApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public ApplicationUser? Host { get; set; }
        public int? GuestId { get; set; }
        public ApplicationUser? Guest { get; set; }
        public Grid? Grid { get; set; }
        public string? Status { get; set; }
    }
}
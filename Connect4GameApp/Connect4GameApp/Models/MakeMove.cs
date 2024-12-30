namespace Connect4GameApp.Models
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public String? GameCode { get; set; }
        public string? PlayerId { get; set; }
        public int Column { get; set; }
    }
}
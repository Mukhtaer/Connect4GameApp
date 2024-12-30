namespace Connect4GameApp.Client.Models
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public string? GameCode { get; set; }
        public int Column { get; set; }
    }
}
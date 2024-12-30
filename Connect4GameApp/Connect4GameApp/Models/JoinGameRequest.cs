namespace Connect4GameApp.Models
{
public class JoinGameRequest
{
    public int GameId { get; set; }
    public string? GuestId { get; set; }
    public string? GameCode { get; set; }
}
}
namespace Connect4GameApp.Client.Models
{
public class JoinGameRequest
{
    public int GameId { get; set; }
    public string? GuestId { get; set; }
    public string? GameCode { get; set; }
    
}
}
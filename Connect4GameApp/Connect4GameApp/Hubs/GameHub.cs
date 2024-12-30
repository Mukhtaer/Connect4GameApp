using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Connect4GameApp.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendMove(string gameId, int column, int player)
        {
            await Clients.Group(gameId).SendAsync("ReceiveMove", column, player);
            Console.WriteLine("Move sent");
        }

        public async Task JoinGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("PlayerJoined");
            Console.WriteLine("Player joined game");
        }

        public async Task LeaveGame(string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
            Console.WriteLine("Player left game");
        }

        public async Task StartGame(string gameId)
        {
            await Clients.Group(gameId).SendAsync("GameStarted");
            Console.WriteLine("Game started");
        }
    }
}
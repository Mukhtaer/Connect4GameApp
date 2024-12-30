using Connect4GameApp.Components.Pages;
using Microsoft.AspNetCore.Identity;

namespace Connect4GameApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Connect4GameApp.Models.Game>? HostGames { get; set; }

        public ICollection<Connect4GameApp.Models.Game>? GuestGames { get; set; }
        
    }

}

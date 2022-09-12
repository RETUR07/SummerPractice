using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Entities.Models;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Hubs
{
    [Authorize]
    public class BaseHub : Hub
    {
        public readonly UserManager<User> _userManager;

        public BaseHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string UserId
        {
            get
            {
                return _userManager.GetUserId(Context.User);
            }
        }
    }
}

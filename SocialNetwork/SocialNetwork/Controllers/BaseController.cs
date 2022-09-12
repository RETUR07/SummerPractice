using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Entities.Models;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [ApiController]
    public class Base : ControllerBase
    {
        public readonly UserManager<User> _userManager;

        public Base(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public string UserId
        {
            get
            {
                return _userManager.GetUserId(HttpContext.User);
            }
        }     
    }
}

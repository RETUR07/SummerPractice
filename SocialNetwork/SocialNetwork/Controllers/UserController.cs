using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Base
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, UserManager<User> userManager)
            : base(userManager)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersdto = await _userService.GetUsersAsync();
            if (usersdto == null) return NotFound();
            return Ok(usersdto);
        }

        [HttpGet("info", Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] UserRegistrationForm userdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }
            var result = _userService.CreateUserAsync(userdto).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UserForm userdto)
        {
            try
            {
                await _userService.UpdateUserAsync(UserId, userdto);
            }
            catch(InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteUser()
        {
            await _userService.DeleteUserAsync(UserId);
            return NoContent();
        }

        [HttpPut("addfriend/{friendId}")]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            await _userService.AddFriendAsync(UserId, friendId);
            return NoContent();
        }

        [HttpPut("deletefriend/{friendId}")]
        public async Task<IActionResult> DeleteFriend(string friendId)
        {
            await _userService.DeleteFriendAsync(UserId, friendId);
            return NoContent();
        }
    }
}

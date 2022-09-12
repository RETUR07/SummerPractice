using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SocialNetwork.Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Base
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService, UserManager<User> userManager)
            : base(userManager)
        {
            _postService = postService;
        }

        [HttpGet("userposts/{userId}")]
        public async Task<IActionResult> GetUserPosts(string userId, [FromQuery] Parameters parameters)
        {
            var postsdto = await _postService.GetPosts(userId, parameters);
            return Ok(postsdto);
        }

        [HttpGet("childposts/{postId}")]
        public async Task<IActionResult> GetChildPosts(int postId, [FromQuery] Parameters parameters)
        {
            var postsdto = await _postService.GetChildPosts(postId, parameters);
            return Ok(postsdto);
        }

        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostForm postdto)
        {
            var post = await _postService.CreatePost(postdto, UserId);
            if (post == null)
            {
                return BadRequest("Post is null");
            }
            return CreatedAtRoute("GetPost", new { postId = post.Id }, await _postService.GetPost(post.Id));
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
            return NoContent();
        }
    }
}

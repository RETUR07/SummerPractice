using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Hubs;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Base
    {
        IHubContext<RateHub> _hubContext;
        private readonly IRateService _rateService;

        public RateController(IRateService rateService, IHubContext<RateHub> hubContext, UserManager<User> userManager)
            : base(userManager)
        {
            _rateService = rateService;
            _hubContext = hubContext;
        }

        [HttpGet("post/{userId}/{postId}")]
        public async Task<IActionResult> GetRate(string userId, int postId)
        {
            var rate = await _rateService.GetPostRateAsync(userId, postId, false);
            if (rate == null)
                return NotFound();
            return Ok(rate);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetRatesOfPost(int postId)
        {
            var rates = await _rateService.GetRatesByPostIdAsync(postId, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetRatesOfPost([FromQuery(Name = "postIDs")] List<int> postIds)
        {
            var rates = await _rateService.GetRatesByPostsIdsAsync(postIds, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpPut("post")]
        public async Task<IActionResult> UpdatePostRate([FromBody]RateForm rateForm)
        {
            if (rateForm == null)
            {
                return BadRequest("RateForm is null");
            }

            await _rateService.UpdatePostRateAsync(rateForm, UserId);
            await _hubContext.Clients.All.SendAsync("Notify", await _rateService.GetPostRateAsync(UserId, rateForm.ObjectId, false));

            return NoContent();
        }
    }
}

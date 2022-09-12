//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SocialNetwork.Application.Contracts;
//using SocialNetwork.Security.DTO;
//using System;
//using System.Threading.Tasks;

//namespace SocialNetwork.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthorizationController : Base
//    {
//        private IAuthService _authorizationService;

//        public AuthorizationController(IAuthService authorizationService)
//        {
//            _authorizationService = authorizationService;
//        }

//        [AllowAnonymous]
//        [HttpPost("authenticate")]
//        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest model)
//        {
//            try
//            {
//                var response = await _authorizationService.AuthenticateAsync(model, ipAddress());
//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [AllowAnonymous]
//        [HttpPost("refresh-token")]
//        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
//        {
//            var response = await _authorizationService.RefreshTokenAsync(refreshToken, ipAddress());
//            return Ok(response);
//        }

//        [HttpPost("revoke-token")]
//        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenRequest model)
//        {
//            var token = model.Token ?? model.RefreshToken;

//            if (string.IsNullOrEmpty(token))
//                return BadRequest(new { message = "Token is required" });

//            await _authorizationService.RevokeTokenAsync(token, ipAddress());
//            return Ok(new { message = "Token revoked" });
//        }

//        [HttpGet("refresh-tokens")]
//        public async Task<IActionResult> GetRefreshTokensAsync()
//        {
//            var refreshTokens = await _authorizationService.GetUserRefreshTokensAsync(UserId);
//            return Ok(refreshTokens);
//        }

//        private string ipAddress()
//        {
//            if (Request.Headers.ContainsKey("X-Forwarded-For"))
//                return Request.Headers["X-Forwarded-For"];
//            else
//                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
//        }
//    }
//}

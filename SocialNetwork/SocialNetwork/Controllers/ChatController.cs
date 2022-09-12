using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SocialNetwork.Application.Exceptions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Entities.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Base
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService, UserManager<User> userManager)
            : base(userManager)
        { 
            _chatService = chatService;
        }

        [HttpGet("chats/")]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatService.GetChats(UserId);
            if (chats == null)
            {
                return BadRequest();
            }
            return Ok(chats);
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _chatService.GetMessages(UserId, chatId);
            if (messages == null) return NotFound();
            return Ok(messages);
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            var chat = await _chatService.GetChat(UserId, chatId);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("chat/{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            await _chatService.DeleteChat(chatId);
            return NoContent();
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            await _chatService.DeleteMessage(UserId, messageId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatForm chatdto)
        {
            var chat = await _chatService.CreateChat(UserId, chatdto);
            if (chat == null)
            {
                return BadRequest("Chat is null");
            }
            return Ok(chat.Id);
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public async Task<IActionResult> AddUser(int chatId, string userId)
        {
            try
            {
                await _chatService.AddUser(chatId, userId, UserId);
            }
            catch(InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }

        [HttpPut("addmessage")]
        public async Task<IActionResult> AddMessage([FromForm]MessageForm messagedto)
        {
            try
            {
                var message = await _chatService.AddMessage(UserId, messagedto);
                return Ok(message);
            }
            catch (InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("addFilesToMessage/{messageId}")]
        public async Task<IActionResult> Add(int messageId, [FromForm] IEnumerable<IFormFile> formFiles)
        {
            var message = await _chatService.AddFilesToMessage(UserId, messageId, formFiles);
            if (message == null) return BadRequest();
            return Ok(message);
        }
    }
}

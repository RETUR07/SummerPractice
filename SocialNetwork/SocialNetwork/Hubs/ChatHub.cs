using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class ChatHub : BaseHub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService, UserManager<User> userManager)
            :base(userManager)
        {
            _chatService = chatService;
        }

        public async Task Subscribe(int chatId)
        {
            if (await _chatService.GetChat(UserId, chatId) == null)
            {
                await Clients.Caller.SendAsync("Notify", "Denied");
            }
            else
            {
                string groupname = "chat" + chatId;
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Caller.SendAsync("Notify", "Subscribed");
            }
        }

        public async Task AddMessage(MessageForm messagedto)
        {
            try
            {
                var message = await _chatService.AddMessage(UserId, messagedto);
                await Clients.Group("chat" + message.ChatId).SendAsync("Send", message);
            }
            catch (InvalidDataException exc)
            {
                await Clients.Caller.SendAsync(exc.Message);
            }
        }

        public async Task MessageChanged(int messageId)
        {
            try
            {
                var message = await _chatService.GetMessage(messageId);
                await Clients.Group("chat" + message.ChatId).SendAsync("MessageChanged", message);
            }
            catch (InvalidDataException exc)
            {
                await Clients.Caller.SendAsync(exc.Message);
            }
        }
    }
}

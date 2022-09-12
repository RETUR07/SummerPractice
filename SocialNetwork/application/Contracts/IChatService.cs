using Microsoft.AspNetCore.Http;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IChatService
    {
        public Task<List<ChatForResponseDTO>> GetChats(string userId);
        public Task<List<MessageForResponseDTO>> GetMessages(string userId, int chatId);
        public Task<ChatForResponseDTO> GetChat(string userId, int chatId);
        public Task DeleteChat(int chatId);
        public Task DeleteMessage(string userId, int messageId);
        public Task<MessageForResponseDTO> GetMessage(int messageId);
        public Task<Chat> CreateChat(string userId, ChatForm chatdto);
        public Task AddUser(int chatId, string userId, string adderId);
        public Task<MessageForResponseDTO> AddMessage(string UserId, MessageForm messagedto);
        public Task<MessageForResponseDTO> AddFilesToMessage(string UserId, int messageId, IEnumerable<IFormFile> formFiles);
    }
}

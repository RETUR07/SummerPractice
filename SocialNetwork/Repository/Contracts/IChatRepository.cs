using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IChatRepository : IRepositoryBase<Chat>
    {
        public Task<List<Chat>> GetChatsAsync(User user, bool trackChanges);
        public Task<Chat> GetChatAsync(int chatId, bool trackChanges);
    }
}

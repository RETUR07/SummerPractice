using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        public Task<List<Message>> GetMessgesByChatIdAsync(int chatId, bool trackChanges);
        public Task<Message> GetMessageAsync(int messageId, bool trackChanges);
    }
}

using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class MessageLogRepository : RepositoryBase<MessageLog>, IMessageLogRepository
    {
        public MessageLogRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public MessageLog GetMessageLog(int id, bool trackChanges) =>
            FindByCondition(m => m.Id == id, trackChanges).SingleOrDefault();
    }
}

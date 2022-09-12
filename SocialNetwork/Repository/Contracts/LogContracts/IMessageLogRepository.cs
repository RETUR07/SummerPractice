using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IMessageLogRepository : IRepositoryBase<MessageLog>
    {
        public MessageLog GetMessageLog(int id, bool trackChanges);
    }
}

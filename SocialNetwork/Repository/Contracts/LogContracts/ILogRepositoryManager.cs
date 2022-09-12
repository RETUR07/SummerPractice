using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface ILogRepositoryManager
    {
        IMessageLogRepository MessageLog { get; }
        Task SaveAsync();
    }
}

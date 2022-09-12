using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository.LogRepository
{
    public class LogRepositoryManager : ILogRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private IMessageLogRepository _messageLogRepository;

        public LogRepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IMessageLogRepository MessageLog
        {
            get
            {
                if (_messageLogRepository == null)
                    _messageLogRepository = new MessageLogRepository(_repositoryContext);
                return _messageLogRepository;
            }
        }
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}

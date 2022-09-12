using SocialNetworks.Repository.Contracts;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private IBlobRepository _blobRepository;
        private IRateRepository _rateRepository;
        private IChatRepository _chatRepository;
        private IMessageRepository _messageRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);
                return _userRepository;
            }
        }

        public IPostRepository Post
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_repositoryContext);
                return _postRepository;
            }
        }

        public IBlobRepository Blob
        {
            get
            {
                if (_blobRepository == null)
                    _blobRepository = new BlobRepository(_repositoryContext);
                return _blobRepository;
            }
        }

        public IRateRepository Rate
        {
            get
            {
                if (_rateRepository == null)
                    _rateRepository = new RateRepository(_repositoryContext);
                return _rateRepository;
            }
        }
        public IChatRepository Chat
        {
            get
            {
                if (_chatRepository == null)
                    _chatRepository = new ChatRepository(_repositoryContext);
                return _chatRepository;
            }
        }
        public IMessageRepository Message
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_repositoryContext);
                return _messageRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}

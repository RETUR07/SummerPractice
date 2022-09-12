using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<List<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(string userId, bool trackChanges);
    }
}

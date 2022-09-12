using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Rate> GetPostRateAsync(string userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.UserId == userId && r.PostId == postId, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<List<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.PostId == postId, trackChanges)
            .ToListAsync();
    }
}

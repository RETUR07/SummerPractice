using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class BlobRepository : RepositoryBase<Blob>, IBlobRepository
    {
        public BlobRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Blob> GetBlob(int blobId, bool trackChanges) =>
             await FindByCondition(b => b.Id == blobId, trackChanges).SingleOrDefaultAsync();

    }
}

using SocialNetwork.Entities.Models;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IBlobRepository : IRepositoryBase<Blob>
    {
        public Task<Blob> GetBlob(int blobId, bool trackChanges);
    }
}

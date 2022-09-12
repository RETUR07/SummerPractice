using SocialNetwork.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IRateService
    {
        public Task<PostRateForResponseDTO> GetPostRateAsync(string userId, int postId, bool trackChanges);
        public Task<List<PostRateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        public Task<List<List<PostRateForResponseDTO>>> GetRatesByPostsIdsAsync(List<int> postIds, bool trackChanges);
        public Task UpdatePostRateAsync(RateForm rate, string userId);
    }
}

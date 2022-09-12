using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto, string userId);
        public Task DeletePost(int postId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<PagedList<PostForResponseDTO>> GetPosts(string userId, Parameters parameters);
        public Task<PagedList<PostForResponseDTO>> GetChildPosts(int postId, Parameters parameters);
    }
}

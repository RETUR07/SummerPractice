using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        public new async void Delete(Post entity)
        {
            if (entity != null)
            {
                foreach (var e in await GetChildrenPostsByPostIdAsync(entity.Id, true))
                {
                    Delete(e);
                }
                base.Delete(entity);
            }
        }

        public PagedList<Post> GetAllPostsPaged(string userId, Parameters parameters, bool trackChanges)
        {
            var posts = FindByCondition(p => p.Author.Id == userId, trackChanges)
            .Include(p => p.Comments)
            .Include(p => p.ParentPost)
            .Include(p => p.BlobIds);
            
            return PagedList<Post>.ToPagedList(posts, parameters.PageNumber, parameters.PageSize);
        }          

        public async Task<Post> GetPostAsync(int postId, bool trackChanges) =>
            await FindByCondition(p => p.Id == postId, trackChanges)
            .Include(p => p.BlobIds)
            .Include(p => p.Author)
            .Include(p => p.ParentPost)
            .Include(p => p.Comments)
            .Include(p => p.ParentPost)
            .SingleOrDefaultAsync();

        public PagedList<Post> GetChildrenPostsByPostIdPaged(int postId, Parameters parameters, bool trackChanges){
            var posts = FindByCondition(r => r.ParentPost.Id == postId, trackChanges).Include(x => x.BlobIds);
            return PagedList<Post>.ToPagedList(posts, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<List<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges) => 
            await FindByCondition(r => r.ParentPost.Id == postId, trackChanges).Include(x => x.BlobIds).ToListAsync();
    }
}

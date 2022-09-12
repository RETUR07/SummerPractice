using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        protected RepositoryContext RepositoryContext;
        private DbSet<User> RepContextSet;

        public UserRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            RepContextSet = RepositoryContext.Set<User>();
        }

        public void Create(User entity) => RepContextSet.Add(entity);


        public void Delete(User entity)
        {
            if (entity.Friends != null)
                entity.Friends.Clear();
            if (entity.MakedFriend != null)
                entity.MakedFriend.Clear();
            if (entity.Subscribed != null)
                entity.Subscribed.Clear();
            if (entity.Subscribers != null)
                entity.Subscribers.Clear();
        }

        public IQueryable<User> FindAll(bool trackChanges) =>
        !trackChanges ?
            RepContextSet
            .Where(e => e.IsEnable)
            .AsNoTracking() :
            RepContextSet;

        public IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            RepContextSet
            .Where(e => e.IsEnable)
            .Where(expression)
            .AsNoTracking() :
            RepContextSet
            .Where(e => e.IsEnable)
            .Where(expression);

        public async Task<List<User>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(u => u.LastName).ToListAsync();

        public async Task<User> GetUserAsync(string userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges)
            .Include(x => x.Friends)
            .Include(x => x.MakedFriend)
            .Include(x => x.Subscribers)
            .Include(x => x.Subscribed)
            .AsSplitQuery()
            .SingleOrDefaultAsync();

        public void Update(User entity)
        {
            if (entity.IsEnable) RepContextSet.Update(entity);
        }
    }
}

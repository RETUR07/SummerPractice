using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SocialNetworks.Repository.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : ParentModel
    {
        protected RepositoryContext RepositoryContext;
        private DbSet<T> RepContextSet;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            RepContextSet = RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
            RepContextSet
            .Where(e => e.IsEnable)
            .AsNoTracking() :
            RepContextSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            RepContextSet
            .Where(e => e.IsEnable)
            .Where(expression)
            .AsNoTracking() :
            RepContextSet
            .Where(e => e.IsEnable)
            .Where(expression);

        public void Create(T entity) => RepContextSet.Add(entity);

        public void Update(T entity)
        {
            if(entity.IsEnable)RepContextSet.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsEnable = false;
            RepContextSet.Update(entity);
        }
        public void NotSoftDelete(T entity) => RepContextSet.Remove(entity);
    }
}

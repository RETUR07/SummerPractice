using System.Collections.Generic;
using Xunit;
using SocialNetworks.Repository.Repository;
using SocialNetwork.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SocialNetworks.Repository.Contracts;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SocialNetwork.Tests
{
    public class PostRepUnitTest : InMemoryDatabase
    {
        public PostRepUnitTest()
            : base(
                new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=PostRepTest.db")
                .Options)
        {
        }
        
        [Fact]
        public async Task GetChildrenPostsByPostIdAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var postRep = new PostRepository(repositoryContext);
                var result = await postRep.GetChildrenPostsByPostIdAsync(2, false);
                Assert.Single(result);
                Assert.Equal(3, result[0].Id);
            }
        }

        [Fact]
        public async Task GetPostAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var postRep = new PostRepository(repositoryContext);
                var result = await postRep.GetPostAsync(1, false);
                Assert.Equal(1, result.Id);
                Assert.Equal("retur 1", result.Author.UserName);
            }
        }
    }
}
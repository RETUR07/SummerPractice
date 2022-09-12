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
    public class UserRepUnitTest : InMemoryDatabase
    {
        public UserRepUnitTest()
        : base(
            new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=UserRepTest.db")
                .Options)
        {
        }

        [Fact]
        public async void GetAllUsersAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.GetAllUsersAsync(false);
                Assert.Equal(3, result.Count);
                Assert.Equal("retur 1", result[0].UserName);
                Assert.Equal("retur 2", result[1].UserName);
                Assert.Equal("retur 3", result[2].UserName);

            }
        }

        [Fact]
        public async Task GetUserAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.GetUserAsync("2", false);
                Assert.Equal("retur 2", result.UserName);
            }
        }

        [Fact]
        public async void DeleteTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var deletionUser = await userRep.GetUserAsync("2", true);
                userRep.Delete(deletionUser);
                repositoryContext.SaveChanges();
                var dbuser = await userRep.GetUserAsync("2", true);
                Assert.Null(dbuser);
                var users = await userRep.GetAllUsersAsync(false);
                Assert.Equal(2, users.Count);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialNetwork.Tests
{
    public class ChatRepUnitTest : InMemoryDatabase
    {
        public ChatRepUnitTest()
            : base(new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=ChatRepTest.db")
                .Options)
        {
        }

        [Fact]
        public async Task GetChatsAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var chatRep = new ChatRepository(repositoryContext);
                var userRep = new UserRepository(repositoryContext);
                var user = await userRep.GetUserAsync("3", false);
                var result = await chatRep.GetChatsAsync(user, false);
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task GetChatAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var chatRep = new ChatRepository(repositoryContext);
                var result = await chatRep.GetChatAsync(2, false);
                Assert.Equal(3, result.Users.Count);
            }
        }
    }
}

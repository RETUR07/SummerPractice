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
    public class MessageRepUnitTest : InMemoryDatabase
    {
        public MessageRepUnitTest()
            : base(new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=MessageRepTest.db")
                .Options)
        {
        }

        [Fact]
        public async Task GetMessgesByChatIdAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var messageRep = new MessageRepository(repositoryContext);
                var result = await messageRep.GetMessgesByChatIdAsync(2, false);
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task GetMessageAsyncTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var messageRep = new MessageRepository(repositoryContext);
                var result = await messageRep.GetMessageAsync(2, false);
                Assert.Equal("2", result.Text);
            }
        }
    }
}

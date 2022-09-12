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
    public class BlobRepUnitTest :InMemoryDatabase
    {
        public BlobRepUnitTest()
        : base(new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase("Filename=BlobRepTest.db")
            .Options)
        {
        }

        [Fact]
        public async Task GetBlobTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var blobRep = new BlobRepository(repositoryContext);
                var result = await blobRep.GetBlob(2, false);
                Assert.Equal("2", result.Filename);
            }
        }
    }
}

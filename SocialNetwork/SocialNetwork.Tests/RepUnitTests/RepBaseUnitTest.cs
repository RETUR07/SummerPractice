using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialNetwork.Tests.RepUnitTests
{
    public class RepBaseUnitTest : InMemoryDatabase
    {
        public RepBaseUnitTest()
        : base(
            new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Filename=RepBaseTest.db")
                .Options)
        {
        }

        [Fact]
        public void FindAllUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = userRep.FindAll(false);
                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void FindByConditionUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = userRep.FindByCondition(x => x.Id == "1", false);
                Assert.Equal(1, result.Count());
                Assert.Equal("1", result.First().Id);
            }
        }

        [Fact]
        public async Task CreateUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                userRep.Create(new User() { UserName = "retur007" });
                await repositoryContext.SaveChangesAsync();
                var result = userRep.FindByCondition(x => x.UserName == "retur007", false);
                Assert.Equal(1, result.Count());
                Assert.Equal("4", result.First().Id);
            }
        }

        [Fact]
        public async Task UpdateUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                result.UserName = "no retur";
                userRep.Update(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                Assert.Equal("1", afterChange.Id);
                Assert.Equal("no retur", afterChange.UserName);
            }
        }

        [Fact]
        public async Task DeleteUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                userRep.Delete(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                Assert.Null(afterChange);
            }
        }

        [Fact]
        public async Task NotSoftDeleteUnitTest()
        {
            using (var repositoryContext = new RepositoryContext(ContextOptions))
            {
                var userRep = new UserRepository(repositoryContext);
                var result = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                userRep.Delete(result);
                await repositoryContext.SaveChangesAsync();
                var afterChange = await userRep.FindByCondition(x => x.Id == "1", false).SingleOrDefaultAsync();
                Assert.Null(afterChange);
            }
        }

        //mock setup

        private UserRepository _repository;

        private List<User> testList = new List<User>();

        private List<User> MockSetup()
        {
            User testObject1 = new User() { Id = "1", IsEnable = true };
            User testObject2 = new User() { Id = "2", IsEnable = true };

            testList.Clear();
            testList.AddRange(new[] { testObject1, testObject2 });

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());
            dbSetMock.Setup(x => x.Add(It.IsAny<User>())).Callback((User user) => testList.Add(user));

            var context = new Mock<RepositoryContext>(new DbContextOptionsBuilder().Options);
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);


            _repository = new UserRepository(context.Object);
            return testList;
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void MockFindByConditionUnitTest(string value)
        {
            MockSetup();
            var result = _repository.FindByCondition(x=>x.Id == value, false);

            Assert.Equal(value, result.ToList()[0].Id);
        }

        [Fact]
        public void MockFindAllUnitTest()
        {
            MockSetup();
            var result = _repository.FindAll(false);
            Assert.Equal(testList, result.ToList());
        }

        [Fact]
        public void MockDeleteUnitTest()
        {
            MockSetup();
            var user = _repository.FindByCondition(x => x.Id == "1", true);
            _repository.Delete(user.ToList()[0]);
            //save
            var result = _repository.FindAll(false);
            Assert.DoesNotContain(testList[0], result);
            Assert.Single(result);
        }

        [Fact]
        public void MockCreateUnitTest()
        {
            MockSetup();
            var user = new User() { Id = "3", IsEnable = true };
            _repository.Create(user);
            //save
            var result = _repository.FindAll(false);
            Assert.Contains(user, result);
        }

        [Fact]
        public void MockUpdateUnitTest()
        {
            MockSetup();
            var users = _repository.FindByCondition(x => x.Id == "1", true);
            var user = users.ToList()[0];
            user.UserName = "retur"; 
            _repository.Update(user);
            //save
            var usersAfter = _repository.FindByCondition(x => x.Id == "1", true);
            Assert.Equal("retur", usersAfter.ToList()[0].UserName);
        }
    }
}

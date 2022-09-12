using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests
{
    public class InMemoryDatabase
    {
        protected InMemoryDatabase(DbContextOptions<RepositoryContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<RepositoryContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new RepositoryContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var user1 = new User() { IsEnable = true, UserName = "retur 1", Id = "1" };
                var user2 = new User() { IsEnable = true, UserName = "retur 2", Id = "2" };
                var user3 = new User() { IsEnable = true, UserName = "retur 3", Id = "3" };

                var post1 = new Post() { Author = user1, Id = 1, IsEnable = true, Header = "1" };
                var post2 = new Post() { Author = user2, ParentPost = post1, Id = 2, IsEnable = true, Header = "2" };
                var post3 = new Post() { Author = user1, ParentPost = post2, Id = 3, IsEnable = true, Header = "3" };

                var rate1 = new Rate(){ UserId = user1.Id, PostId = post1.Id, Id = 1, IsEnable = true };
                var rate2 = new Rate(){ UserId = user1.Id, PostId = post2.Id, Id = 2, IsEnable = true };
                var rate3 = new Rate(){ UserId = user2.Id, PostId = post2.Id, Id = 3, IsEnable = true };               

                var chat1 = new Chat() { Id = 1, IsEnable = true, Users = new List<User>() { user1, user2 }, Messages = new List<Message>() { } };
                var chat2 = new Chat() { Id = 2, IsEnable = true, Users = new List<User>() { user1, user2, user3 }, Messages = new List<Message>() { } };
                var chat3 = new Chat() { Id = 3, IsEnable = true, Users = new List<User>() { user2, user3 }, Messages = new List<Message>() { } };

                var message1 = new Message() { User = user1, Chat = chat1, Id = 1, IsEnable = true, Text = "1" };
                var message2 = new Message() { User = user2, Chat = chat2, Id = 2, IsEnable = true, Text = "2" };
                var message3 = new Message() { User = user3, Chat = chat2, Id = 3, IsEnable = true, Text = "3" };

                chat1.Messages.Add(message1);
                chat2.Messages.Add(message2);
                chat2.Messages.Add(message3);

                var blob1 = new Blob(){ Id = 1, Filename = "1", IsEnable = true };
                var blob2 = new Blob(){ Id = 2, Filename = "2", IsEnable = true };
                var blob3 = new Blob(){ Id = 3, Filename = "3", IsEnable = true };

                context.AddRange(user1, user2, user3);
                context.AddRange(post1, post2, post3);
                context.AddRange(rate1, rate2, rate3);
                context.AddRange(chat1, chat2, chat3);
                context.AddRange(message1, message2, message3);
                context.AddRange(blob1, blob2, blob3);
                context.SaveChanges();
            }
        }
    }
}

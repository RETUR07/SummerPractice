using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using System;

namespace SocialNetworks.Repository.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Rate>()
                .Property(e => e.LikeStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (LikeStatus)Enum.Parse(typeof(LikeStatus), v));

            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany(u => u.MakedFriend);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Subscribers)
                .WithMany(u => u.Subscribed);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.User);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(ch => ch.Users);

            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.ParentPost);
        }
    }
}

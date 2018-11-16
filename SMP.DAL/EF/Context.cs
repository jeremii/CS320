using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SMP.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SMP.DAL.EF
{
    public class Context : IdentityDbContext<IdentityUser>
    {

        private string connectionString = @"Server=localhost;user=sa;password=CitSaPw!;MultipleActiveResultSets=true;";
        //private string connectionString = @"Server=(LocalDb)\v11.0;user=sa;password=CitSaPw!;MultipleActiveResultSets=true;";
        //private string connectionString = @"Server=(localdb)\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true;";
        
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Rss> Rss { get; set; }
        public DbSet<Message> Message { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception)
            {
                //Should do something meaningful here                
            }
        }

        public Context()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // EnableRetryOnFailure adds default SqlServerRetryingExecutionStrategy
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>()
                        .HasKey(a => new { a.UserId, a.FollowerId });
            modelBuilder.Entity<Follow>()
                .HasOne(e => e.User)
                .WithMany(e => e.Follows)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>(e =>
            {
                e.Property(p => p.Time)
                .HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<Message>()
                .HasKey(key => new { key.Id, key.SenderId, key.ReceiverId });

            modelBuilder.Entity<Message>()
                        .HasOne( e => e.Receiver)
                        .WithMany(e => e.ReceivedMessages)
                        .HasForeignKey(e => e.ReceiverId)
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
                        .HasOne(e => e.Sender)
                        .WithMany(e => e.SentMessages)
                        .HasForeignKey(e => e.SenderId)
                        .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Customer>(entity =>
            //{
            //    entity.HasIndex(e => e.EmailAddress).HasName("IX_Customers").IsUnique();
            //});

            //modelBuilder.Entity<Order>(entity =>
            //{
            //    entity.Property(e => e.OrderDate)
            //      .HasColumnType("datetime")
            //      .HasDefaultValueSql("getdate()");
            //    entity.Property(e => e.ShipDate)
            //      .HasColumnType("datetime")
            //      .HasDefaultValueSql("getdate()");
            //    entity.Property(e => e.OrderTotal)
            //      .HasColumnType("money")
            //      .HasComputedColumnSql("Store.GetOrderTotal([Id])");
            //});


            //modelBuilder.Entity<OrderDetail>(entity =>
            //{
            //    entity.Property(e => e.LineItemTotal)
            //      .HasColumnType("money")
            //      .HasComputedColumnSql("[Quantity]*[UnitCost]");
            //    entity.Property(e => e.UnitCost).HasColumnType("money");
            //});


            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.Property(e => e.UnitCost).HasColumnType("money");
            //    entity.Property(e => e.CurrentPrice).HasColumnType("money");
            //});

            //modelBuilder.Entity<ShoppingCartRecord>(entity =>
            //{
            //    entity.HasIndex(e => new { ShoppingCartRecordId = e.Id, e.ProductId, e.CustomerId })
            //         .HasName("IX_ShoppingCart")
            //         .IsUnique();
            //    entity.Property(e => e.DateCreated)
            //      .HasColumnType("datetime")
            //      .HasDefaultValueSql("getdate()");
            //    entity.Property(e => e.Quantity).HasDefaultValue(1);
            //});
        }
    }
}
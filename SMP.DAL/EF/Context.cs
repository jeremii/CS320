using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SMP.Models.Entities;

namespace SMP.DAL.EF
{
    public class Context : IdentityDbContext<ApplicationUser>
    {


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
        private string connectionString = @"Server=localhost;user=sa;password=CitSaPw!;MultipleActiveResultSets=true;";
        //private string connectionString = @"Server=(LocalDb)\v11.0;user=sa;password=CitSaPw!;MultipleActiveResultSets=true;";

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
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<IdentityUserLogin<string>>();
            //modelBuilder.Ignore<IdentityUserRole<string>>();
            //modelBuilder.Ignore<IdentityUserClaim<string>>();
            //modelBuilder.Ignore<IdentityUserToken<string>>();
            //modelBuilder.Ignore<IdentityUser<string>>();
            //modelBuilder.Ignore<ApplicationUser>();
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
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Follow> Follow { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Infra.Contexts
{
    public class ModernStoreDataContext : DbContext
    {
        public ModernStoreDataContext(DbContextOptions<ModernStoreDataContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Persist Security Info=False;User ID=sa;Password=sa;Server=.\SQLEXPRESS;Database=ModernStore",
                    options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<FluentValidator.Validation.Notification>();
            modelBuilder.Ignore<Domain.ValueObjects.Document>();
            modelBuilder.Ignore<Domain.ValueObjects.Email>();

        //https://weblogs.asp.net/ricardoperes/implementing-missing-features-in-entity-framework-core-part-7-entity-configuration-in-mapping-classes
        //https://stackoverflow.com/questions/43070306/entity-framework-core-configurations/43074607
            modelBuilder.ApplyConfiguration<Customer>(new CustomerMap());
            modelBuilder.ApplyConfiguration<Order>(new OrderMap());
            modelBuilder.ApplyConfiguration<OrderItem>(new OrderItemMap());
            modelBuilder.ApplyConfiguration<Product>(new ProductMap());

            //modelBuilder.Entity<Customer>()
            //    .HasKey(entity => entity.Id)
            //    .HasName("PK_CustomerID");

            //modelBuilder.Entity<Order>()
            //    .HasKey(entity => entity.Id)
            //    .HasName("PK_OrderID");

            //modelBuilder.Entity<OrderItem>()
            //    .HasKey(entity => entity.Id)
            //    .HasName("PK_OrderItemID");

            //modelBuilder.Entity<Product>()
            //    .HasKey(entity => entity.Id)
            //    .HasName("PK_ProductID");

            modelBuilder.Entity<User>()
                .HasKey(entity => entity.Id)
                .HasName("PK_UserID");

            base.OnModelCreating(modelBuilder);

        }
    }

}

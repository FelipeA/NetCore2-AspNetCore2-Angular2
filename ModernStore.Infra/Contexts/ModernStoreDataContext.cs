using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            //optionsBuilder
            //    .UseSqlServer(
            //        @"Persist Security Info=False;User ID=sa;Password=sa;Server=.\SQLEXPRESS;Database=ModernStore",
            //        options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<FluentValidator.Notification>();
            modelBuilder.Ignore<Domain.ValueObjects.Document>();
            modelBuilder.Ignore<Domain.ValueObjects.Email>();

            //https://weblogs.asp.net/ricardoperes/implementing-missing-features-in-entity-framework-core-part-7-entity-configuration-in-mapping-classes
            //https://stackoverflow.com/questions/43070306/entity-framework-core-configurations/43074607
            modelBuilder.ApplyConfiguration<Customer>(new CustomerMap());
            modelBuilder.ApplyConfiguration<Order>(new OrderMap());
            modelBuilder.ApplyConfiguration<OrderItem>(new OrderItemMap());
            modelBuilder.ApplyConfiguration<Product>(new ProductMap());
            modelBuilder.ApplyConfiguration<User>(new UserMap());

            base.OnModelCreating(modelBuilder);

        }
    }

    public class ModernStoreDataContextFactory : IDesignTimeDbContextFactory<ModernStoreDataContext>
    {
        public ModernStoreDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModernStoreDataContext>();

            return new ModernStoreDataContext(optionsBuilder.Options);
        }
    }

}

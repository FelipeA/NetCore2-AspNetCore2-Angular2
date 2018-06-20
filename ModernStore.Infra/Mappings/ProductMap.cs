using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Product");


            entityTypeBuilder.HasKey(entity => entity.Id)
                .HasName("PK_ProductID");

            entityTypeBuilder.Property(x => x.Image).IsRequired().HasMaxLength(1024);
            entityTypeBuilder.Property(x => x.Price).HasColumnType("money");
            entityTypeBuilder.Property(x => x.QuantityOnHand);
            entityTypeBuilder.Property(x => x.Title).IsRequired().HasMaxLength(80);

        }
    }
}

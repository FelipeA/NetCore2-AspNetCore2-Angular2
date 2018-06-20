using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Order");
             
            entityTypeBuilder.HasKey(entity => entity.Id)
                .HasName("PK_OrderID");

            entityTypeBuilder.Property(x => x.CreateDate);
            entityTypeBuilder.Property(x => x.DeliveryFee).HasColumnType("money");
            entityTypeBuilder.Property(x => x.Discount).HasColumnType("money");
            entityTypeBuilder.Property(x => x.Number).IsRequired().HasMaxLength(8);
            entityTypeBuilder.Property(x => x.Status);

            entityTypeBuilder.HasMany(x => x.Items);
            entityTypeBuilder.HasOne(x => x.Customer);
        }
    }
}


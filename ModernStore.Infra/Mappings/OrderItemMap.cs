using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("OrderItem");

            entityTypeBuilder.HasKey(entity => entity.Id)
                .HasName("PK_OrderItemID");

            entityTypeBuilder.Property(x => x.Price).HasColumnType("money");
            entityTypeBuilder.Property(x => x.Quantity);

            //entityTypeBuilder.HasOne(x => x.Product);

        }
    }
}

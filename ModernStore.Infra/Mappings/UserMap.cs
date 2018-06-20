using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("User");
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_UserID");

            entityTypeBuilder.Property(x => x.Active);
            entityTypeBuilder.Property(x => x.Password);
            entityTypeBuilder.Property(x => x.Username);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ModernStore.Infra.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            
            entityTypeBuilder.ToTable("Customer");
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_CustomerID");

            entityTypeBuilder.Property(x => x.BirthDate);

            //https://medium.com/@fabiogemignani/mapeando-entidades-de-valor-no-entity-framework-core-2-7c1dc17b0134
            entityTypeBuilder.OwnsOne<Document>(x => x.Document, d => {

                d.Property(p => p.Number)
                    .HasColumnName("DocumentNumer")
                    .HasMaxLength(30)
                    .IsRequired();
            });

            entityTypeBuilder.OwnsOne<Email>(x => x.Email, e =>
            {
                e.Property(p => p.Address).IsRequired()
                .HasMaxLength(160)
                .HasColumnName("Email");
            });

            entityTypeBuilder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(60);

            entityTypeBuilder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(60);


            entityTypeBuilder.Property(x => x.User.Username)
                .IsRequired()
                .HasMaxLength(20);
            entityTypeBuilder.Property(x => x.User.Password)
                .IsRequired()
                .HasMaxLength(32);
            entityTypeBuilder.Property(x => x.User.Active);
        }
    }
}
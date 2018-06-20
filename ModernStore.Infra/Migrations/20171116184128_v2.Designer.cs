﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ModernStore.Domain.Enums;
using ModernStore.Infra.Contexts;
using System;

namespace ModernStore.Infra.Migrations
{
    [DbContext(typeof(ModernStoreDataContext))]
    [Migration("20171116184128_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerId");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("CustomerId");

                    b.Property<decimal>("DeliveryFee")
                        .HasColumnType("money");

                    b.Property<decimal>("Discount")
                        .HasColumnType("money");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<int>("Status");

                    b.HasKey("Id")
                        .HasName("PK_OrderID");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<Guid?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id")
                        .HasName("PK_OrderItemID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("QuantityOnHand");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id")
                        .HasName("PK_ProductID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id")
                        .HasName("PK_UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Number")
                                .HasColumnName("DocumentNumber");

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Document")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Document", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasMaxLength(160);

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Email")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Email", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("ModernStore.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}

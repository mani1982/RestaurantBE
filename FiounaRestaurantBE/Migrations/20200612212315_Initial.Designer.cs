﻿// <auto-generated />
using System;
using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FiounaRestaurantBE.Migrations
{
    [DbContext(typeof(FiounaRestaurantDbContext))]
    [Migration("20200612212315_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FiounaRestaurantBE.Model.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("zipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAvalable")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OrderDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductCategory")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderDetailId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.Address", b =>
                {
                    b.HasOne("FiounaRestaurantBE.Model.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.Order", b =>
                {
                    b.HasOne("FiounaRestaurantBE.Model.User", "Customer")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.OrderDetail", b =>
                {
                    b.HasOne("FiounaRestaurantBE.Model.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FiounaRestaurantBE.Model.Product", b =>
                {
                    b.HasOne("FiounaRestaurantBE.Model.OrderDetail", "OrderDetail")
                        .WithMany("Products")
                        .HasForeignKey("OrderDetailId");
                });
#pragma warning restore 612, 618
        }
    }
}

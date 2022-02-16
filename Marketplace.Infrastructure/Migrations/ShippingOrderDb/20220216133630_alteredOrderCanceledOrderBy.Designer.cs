﻿// <auto-generated />
using Marketplace.Infrastructure.Shipping.OrderPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marketplace.Infrastructure.Migrations.ShippingOrderDb
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20220216133630_alteredOrderCanceledOrderBy")]
    partial class alteredOrderCanceledOrderBy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Shipping.Order")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Marketplace.Infrastructure.Shipping.OrderPersistence.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CanceledOrderBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Marketplace.Infrastructure.Shipping.OrderPersistence.Status", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = "ce5670ac-a922-4a06-ab9a-3d9b229ff43e",
                            Name = "Delivered"
                        },
                        new
                        {
                            Id = "092a783c-11d2-43d4-bd93-6d1e1a237826",
                            Name = "Shipped"
                        },
                        new
                        {
                            Id = "690fe63a-df4f-42d1-adbd-d2603523559d",
                            Name = "Processing"
                        },
                        new
                        {
                            Id = "a51e6c04-60aa-44dc-828d-d9bba7980357",
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Marketplace.Infrastructure.Shipping.OrderPersistence.Order", b =>
                {
                    b.HasOne("Marketplace.Infrastructure.Shipping.OrderPersistence.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}

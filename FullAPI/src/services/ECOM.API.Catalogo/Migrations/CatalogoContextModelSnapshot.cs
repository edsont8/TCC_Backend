﻿// <auto-generated />
using System;
using ECOM.API.Catalogo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECOM.API.Catalogo.Migrations
{
    [DbContext(typeof(CatalogoContext))]
    partial class CatalogoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECOM.API.Catalogo.Models.AssociatedProducts", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProductFatherId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProductSonId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProductFatherId");

                    b.HasIndex("ProductSonId");

                    b.ToTable("AssociatedProducts");
                });

            modelBuilder.Entity("ECOM.API.Catalogo.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ECOM.API.Catalogo.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(MAX)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(9,2)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ECOM.API.Catalogo.Models.AssociatedProducts", b =>
                {
                    b.HasOne("ECOM.API.Catalogo.Models.Product", "ProductFather")
                        .WithMany("AssociatedProducts")
                        .HasForeignKey("ProductFatherId")
                        .IsRequired();

                    b.HasOne("ECOM.API.Catalogo.Models.Product", "ProductSon")
                        .WithMany()
                        .HasForeignKey("ProductSonId")
                        .IsRequired();
                });

            modelBuilder.Entity("ECOM.API.Catalogo.Models.Product", b =>
                {
                    b.HasOne("ECOM.API.Catalogo.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

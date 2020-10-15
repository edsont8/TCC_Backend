﻿using ECOM.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECOM.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(200)");
            builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(1000)");
            builder.Property(p => p.Model).IsRequired().HasColumnType("varchar(50)");
            builder.Property(p => p.Brand).IsRequired().HasColumnType("varchar(50)");
            builder.Property(p => p.Price).HasColumnType("decimal(9,2)");
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.Image).HasColumnType("varbinary(MAX)");
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Active).IsRequired();

            // 1 : 1 => Product : Category
            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);

            //N : M => Product : AssociatedProducts
            builder.HasMany(p => p.AssociatedProducts).WithOne(y => y.ProductFather);

            builder.ToTable("Products");
        }
    }
}

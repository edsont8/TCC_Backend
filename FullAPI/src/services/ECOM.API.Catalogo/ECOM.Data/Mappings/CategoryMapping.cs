﻿using ECOM.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECOM.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(20)");

            // 1: N => Categoria : Produtos
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category);
                

            builder.ToTable("Categories");
        }
    }
}

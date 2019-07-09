using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.VSolTables.Domain.Entities;

namespace Vsol.Api.VSolTables.Infra.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<ProductInfo>
    {
        void IEntityTypeConfiguration<ProductInfo>.Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(12, 2)");
            builder.Property(x => x.UnitsInStock).HasColumnType("decimal(12, 3)");
            builder.Property(x => x.Discontinued).IsRequired().HasColumnType("bit");

            builder.HasKey(x => x.Id);
            builder.ToTable("Product", "dbo");
        }
    }
}

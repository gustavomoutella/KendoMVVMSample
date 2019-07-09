using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.AppSecurity.Domain.Entities;

namespace Vsol.Api.AppSecurity.Infra.Data.Mappings
{
	public class FeatureMap : IEntityTypeConfiguration<FeatureInfo>
    {
        void IEntityTypeConfiguration<FeatureInfo>.Configure(EntityTypeBuilder<FeatureInfo> builder)
        {
			builder.Property(x => x.IdFeature).IsRequired();
			builder.Property(x => x.IdFeatureParent);
			builder.Property(x => x.FeatureName).IsRequired().HasMaxLength(200);
			builder.Property(x => x.FeatureKey).HasMaxLength(200);
			builder.Property(x => x.Description).HasMaxLength(3000);
			builder.Property(x => x.RecursiveName).IsRequired().HasMaxLength(2000);
			
			builder.HasKey(x => x.IdFeature);
			builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.IdFeatureParent).OnDelete(DeleteBehavior.Restrict);
			builder.ToTable("Feature", "AppSecurity");
		}
	}
}
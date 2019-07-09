using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.AppSecurity.Domain.Entities;

namespace Vsol.Api.AppSecurity.Infra.Data.Mappings
{
	public class AuthorizationMap : IEntityTypeConfiguration<AuthorizationInfo>
    {
        void IEntityTypeConfiguration<AuthorizationInfo>.Configure(EntityTypeBuilder<AuthorizationInfo> builder)
        {
			builder.Property(x => x.IdAuthorization).IsRequired();
			builder.Property(x => x.IdFeature).IsRequired();
			builder.Property(x => x.IdRole).IsRequired();
			builder.Property(x => x.Authorized).IsRequired();
			
			builder.HasKey(x => x.IdAuthorization);
			builder.HasOne(x => x.Feature).WithMany(x => x.Authorizations).HasForeignKey(x => x.IdFeature).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(x => x.Role).WithMany(x => x.Authorizations).HasForeignKey(x => x.IdRole).OnDelete(DeleteBehavior.Restrict);
			builder.ToTable("Authorization", "AppSecurity");
		}
	}
}
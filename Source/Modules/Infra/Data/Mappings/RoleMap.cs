using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.AppSecurity.Domain.Entities;

namespace Vsol.Api.AppSecurity.Infra.Data.Mappings
{
	public class RoleMap : IEntityTypeConfiguration<RoleInfo>
    {
        void IEntityTypeConfiguration<RoleInfo>.Configure(EntityTypeBuilder<RoleInfo> builder)
        {
			builder.Property(x => x.IdRole).IsRequired();
			builder.Property(x => x.RoleName).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Description).HasMaxLength(3000);
			builder.Property(x => x.Enabled).IsRequired();
			
			builder.HasKey(x => x.IdRole);
			builder.ToTable("Role", "AppSecurity");
		}
	}
}
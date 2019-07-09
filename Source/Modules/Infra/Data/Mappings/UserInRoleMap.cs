using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.AppSecurity.Domain.Entities;

namespace Vsol.Api.AppSecurity.Infra.Data.Mappings
{
	public class UserInRoleMap : IEntityTypeConfiguration<UserInRoleInfo>
    {
        void IEntityTypeConfiguration<UserInRoleInfo>.Configure(EntityTypeBuilder<UserInRoleInfo> builder)
        {
			builder.Property(x => x.IdRole).IsRequired();
			builder.Property(x => x.IdUser).IsRequired();
			
			builder.HasKey(x => new { x.IdRole, x.IdUser });
			builder.HasOne(x => x.Role).WithMany(x => x.UsersInRoles).HasForeignKey(x => x.IdRole).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(x => x.User).WithMany(x => x.UsersInRoles).HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.Restrict);
			builder.ToTable("UserInRole", "AppSecurity");
		}
	}
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vsol.Api.AppSecurity.Domain.Entities;

namespace Vsol.Api.AppSecurity.Infra.Data.Mappings
{
	public class UserMap : IEntityTypeConfiguration<UserInfo>
    {
        void IEntityTypeConfiguration<UserInfo>.Configure(EntityTypeBuilder<UserInfo> builder)
        {
			builder.Property(x => x.IdUser).IsRequired();
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
			builder.Property(x => x.LastName).HasMaxLength(200);
			builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Password).HasMaxLength(2000);
			builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
			builder.Property(x => x.EmailConfirmed).IsRequired();
			builder.Property(x => x.LogonDate);
			builder.Property(x => x.LastActionDate);
			builder.Property(x => x.CreationDate).IsRequired();
			builder.Property(x => x.InvalidLogonAmount).IsRequired();
			builder.Property(x => x.Enabled).IsRequired();
			builder.Property(x => x.Blocked).IsRequired();
			builder.Property(x => x.SecurityKey).HasMaxLength(400);
            builder.Property(x => x.IdPessoa).HasColumnType("int");

            builder.HasKey(x => x.IdUser);
            builder.ToTable("User", "AppSecurity");
		}
	}
}
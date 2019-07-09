using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Vsol.Api.Shared.Infra.Data.Contexts;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Infra.Data.Mappings;

namespace Vsol.Api.AppSecurity.Infra.Data.Contexts
{
	public class AppSecurityDataContext : DataContext
	{
		private DbConnection _conn;
		
		public AppSecurityDataContext(IUnitOfWork uow)
		{
            _conn = uow.Connection;
            uow.AddContext(this);
            InitConfigurations();
		}
		
		public DbSet<AuthorizationInfo> Authorization { get; set; }
		
		public DbSet<FeatureInfo> Feature { get; set; }
		
		public DbSet<RoleInfo> Role { get; set; }
		
		public DbSet<UserInfo> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_conn);
		}
		
		protected override void OnModelCreating(ModelBuilder builder)
		{
			RegisterMaps(builder);
		}
		
		public static void RegisterMaps(ModelBuilder builder)
		{
            builder.ApplyConfiguration(new AuthorizationMap());
            builder.ApplyConfiguration(new FeatureMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserInRoleMap());
		}
	}
}
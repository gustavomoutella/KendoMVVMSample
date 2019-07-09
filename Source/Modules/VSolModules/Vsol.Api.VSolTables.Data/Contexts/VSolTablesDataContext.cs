using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Vsol.Api.Shared.Infra.Data.Contexts;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.VSolTables.Domain.Entities;
using Vsol.Api.VSolTables.Infra.Data.Mappings;

namespace Vsol.Api.VSolTables.Infra.Data.Contexts
{
    public class VSolTablesDataContext : DataContext
    {
        private DbConnection _conn;

        public VSolTablesDataContext(IUnitOfWork uow)
        {
            _conn = uow.Connection;
            uow.AddContext(this);
            InitConfigurations();
        }

        public DbSet<ProductInfo> Product { get; set; }

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
            builder.ApplyConfiguration(new ProductMap());
        }
    }
}

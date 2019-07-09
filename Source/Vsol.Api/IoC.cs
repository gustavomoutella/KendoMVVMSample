using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api
{
    public sealed class IoC
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            Vsol.Api.AppSecurity.Infra.CrossCutting.IoC.AppSecurityIoC.Register(services);
            Vsol.Api.VSolTables.Infra.CrossCutting.IoC.VSolTablesIoC.Register(services);
        }
    }
}

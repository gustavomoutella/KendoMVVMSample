using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Vsol.Api.VSolTables.Infra.Data.Contexts;

namespace Vsol.Api.VSolTables.Infra.CrossCutting.IoC
{
    internal class DataContext
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<VSolTablesDataContext, VSolTablesDataContext>();
        }
    }
}

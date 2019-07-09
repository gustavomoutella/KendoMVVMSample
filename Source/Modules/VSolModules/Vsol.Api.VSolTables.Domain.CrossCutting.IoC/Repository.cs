using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Vsol.Api.VSolTables.Domain.Repositories;
using Vsol.Api.VSolTables.Infra.Data.Repositories;

namespace Vsol.Api.VSolTables.Infra.CrossCutting.IoC
{
    internal class Repository
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}

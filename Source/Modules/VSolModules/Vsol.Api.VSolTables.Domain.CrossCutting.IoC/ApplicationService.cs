using Microsoft.Extensions.DependencyInjection;
using System;
using Vsol.Api.VSolTables.Application.Services;
using Vsol.Api.VSolTables.Domain.Services;

namespace Vsol.Api.VSolTables.Infra.CrossCutting.IoC
{
    internal class ApplicationService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IProductApplicationService, ProductApplicationService>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Vsol.Api.VSolTables.Domain.Commands.Handlers;

namespace Vsol.Api.VSolTables.Infra.CrossCutting.IoC
{
    internal class CommandHandler
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ProductCommandHandler, ProductCommandHandler>();
        }
    }
}

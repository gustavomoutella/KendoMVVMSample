using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.VSolTables.Infra.CrossCutting.IoC
{
    public sealed class VSolTablesIoC
    {
        public static void Register(IServiceCollection services)
        {
            DataContext.Register(services);
            Repository.Register(services);
            CommandHandler.Register(services);
            ApplicationService.Register(services);
        }
    }
}

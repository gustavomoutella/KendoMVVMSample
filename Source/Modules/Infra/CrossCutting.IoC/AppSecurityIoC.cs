using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vsol.Api.AppSecurity.Infra.CrossCutting.IoC
{
	public sealed class AppSecurityIoC
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

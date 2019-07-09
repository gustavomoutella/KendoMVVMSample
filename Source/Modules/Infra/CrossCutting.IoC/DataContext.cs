using Microsoft.Extensions.DependencyInjection;
using System;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;

namespace Vsol.Api.AppSecurity.Infra.CrossCutting.IoC
{
	internal class DataContext
	{
		public static void Register(IServiceCollection services)
		{
			services.AddScoped<AppSecurityDataContext, AppSecurityDataContext>();
		}
	}
}

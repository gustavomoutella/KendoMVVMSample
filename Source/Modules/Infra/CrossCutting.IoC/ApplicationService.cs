using Microsoft.Extensions.DependencyInjection;
using System;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.AppSecurity.Application.Services;

namespace Vsol.Api.AppSecurity.Infra.CrossCutting.IoC
{
	internal class ApplicationService
	{
		public static void Register(IServiceCollection services)
		{
			services.AddTransient<IAuthorizationApplicationService, AuthorizationApplicationService>();
			services.AddTransient<IFeatureApplicationService, FeatureApplicationService>();
			services.AddTransient<IRoleApplicationService, RoleApplicationService>();
			services.AddTransient<IUserApplicationService, UserApplicationService>();
        }
	}
}

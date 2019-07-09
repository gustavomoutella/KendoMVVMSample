using Microsoft.Extensions.DependencyInjection;
using System;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Infra.Data.Repositories;

namespace Vsol.Api.AppSecurity.Infra.CrossCutting.IoC
{
	internal class Repository
	{
		public static void Register(IServiceCollection services)
		{
			services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
			services.AddTransient<IFeatureRepository, FeatureRepository>();
			services.AddTransient<IRoleRepository, RoleRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
        }
	}
}

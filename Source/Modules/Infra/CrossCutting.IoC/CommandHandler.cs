using System;
using Microsoft.Extensions.DependencyInjection;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;

namespace Vsol.Api.AppSecurity.Infra.CrossCutting.IoC
{
	internal class CommandHandler
	{
		public static void Register(IServiceCollection services)
		{
			services.AddTransient<AuthorizationCommandHandler, AuthorizationCommandHandler>();
			services.AddTransient<FeatureCommandHandler, FeatureCommandHandler>();
			services.AddTransient<RoleCommandHandler, RoleCommandHandler>();
			services.AddTransient<UserCommandHandler, UserCommandHandler>();
        }
	}
}

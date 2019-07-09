using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;

namespace Vsol.Api.AppSecurity.Domain.Specs
{
	public static class AuthorizationSpecs
	{
        public static readonly Expression<Func<AuthorizationInfo, SelectListAuthorizationCommandResult>> AsSelectListAuthorizationCommandResult = x => new SelectListAuthorizationCommandResult
        {
            IdAuthorization = x.IdAuthorization,
            FeatureKey = x.Feature.FeatureKey,
            Authorized = x.Authorized,
            RecursiveName = x.Feature == null ? null : x.Feature.RecursiveName
        };

        public static readonly Expression<Func<AuthorizationInfo, PageAuthorizationCommandResult>> AsPageAuthorizationCommandResult = x => new PageAuthorizationCommandResult
		{
			IdAuthorization = x.IdAuthorization,
			IdFeature = x.IdFeature,
			IdRole = x.IdRole,
			Authorized = x.Authorized,
			Feature = x.Feature == null ? null : new SelectListFeatureCommandResult
			{
				IdFeature = x.IdFeature, 
				FeatureName = x.Feature.FeatureName,
                RecursiveName = x.Feature.RecursiveName
			},
			Role = x.Role == null ? null : new SelectListRoleCommandResult
			{
				IdRole = x.IdRole, 
				RoleName = x.Role.RoleName
			}
		};
		
		public static readonly Expression<Func<AuthorizationInfo, AuthorizationCommandResult>> AsAuthorizationCommandResult = x => new AuthorizationCommandResult
		{
			IdAuthorization = x.IdAuthorization,
			IdFeature = x.IdFeature,
			IdRole = x.IdRole,
			Authorized = x.Authorized
		};
		
		public static Expression<Func<AuthorizationInfo, bool>> TextToSearch(string textToSearch)
		{
			return x => (
                x.Feature.Description.Contains(textToSearch) ||
                x.Feature.FeatureKey.Contains(textToSearch) ||
                x.Feature.FeatureName.Contains(textToSearch) ||
                x.Role.Description.Contains(textToSearch)
            );
		}
	}
}
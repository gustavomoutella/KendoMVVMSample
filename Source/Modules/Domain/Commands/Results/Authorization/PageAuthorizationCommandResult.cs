using System;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization
{
	public class PageAuthorizationCommandResult
	{
		public Guid IdAuthorization { get; set; }
		
		public Guid IdFeature { get; set; }
		
		public Guid IdRole { get; set; }
		
		public bool Authorized { get; set; }
		
		public SelectListFeatureCommandResult Feature { get; set; }
		
		public SelectListRoleCommandResult Role { get; set; }
	}
}
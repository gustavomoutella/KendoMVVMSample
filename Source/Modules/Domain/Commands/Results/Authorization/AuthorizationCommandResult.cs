using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization
{
	public class AuthorizationCommandResult
	{
		public Guid IdAuthorization { get; set; }
		
		public Guid IdFeature { get; set; }
		
		public Guid IdRole { get; set; }
		
		public bool Authorized { get; set; }
		
		public FeatureCommandResult Feature { get; set; }
		
		public RoleCommandResult Role { get; set; }
	}
}
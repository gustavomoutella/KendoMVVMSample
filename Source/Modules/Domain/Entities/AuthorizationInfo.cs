using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Entities
{
	public class AuthorizationInfo : EntityInfo<AuthorizationInfo>
	{
		public AuthorizationInfo() { }
		
		public AuthorizationInfo(InsertAuthorizationCommand command)
		{
			Map(command, this);
			IdAuthorization = Guid.NewGuid();
		}
		
		public AuthorizationInfo(UpdateAuthorizationCommand command)
		{
			Map(command, this);
		}
		
		public Guid IdAuthorization { get; private set; }
		
		public Guid IdFeature { get; private set; }
		
		public Guid IdRole { get; private set; }
		
		public bool Authorized { get; private set; }
		
		#region Navigation properties
		
		public virtual FeatureInfo Feature { get; set; }
		
		public virtual RoleInfo Role { get; set; }
		
		#endregion
	}
}
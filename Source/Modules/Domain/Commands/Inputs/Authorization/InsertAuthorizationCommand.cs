using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization
{
	public class InsertAuthorizationCommand : InputCommand
	{
		public Guid IdFeature { get; set; }
		
		public Guid IdRole { get; set; }
		
		public bool Authorized { get; set; }
	}
}
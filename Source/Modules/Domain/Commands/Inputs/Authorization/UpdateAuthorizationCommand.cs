using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization
{
	public class UpdateAuthorizationCommand : InsertAuthorizationCommand
	{
		public Guid IdAuthorization { get; set; }
	}
}
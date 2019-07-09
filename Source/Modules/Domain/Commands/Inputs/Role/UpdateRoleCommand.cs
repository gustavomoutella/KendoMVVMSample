using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role
{
	public class UpdateRoleCommand : InsertRoleCommand
	{
		public Guid IdRole { get; set; }
	}
}
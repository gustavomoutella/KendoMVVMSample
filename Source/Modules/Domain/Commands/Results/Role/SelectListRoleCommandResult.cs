using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Role
{
	public class SelectListRoleCommandResult
	{
		public Guid IdRole { get; set; }
		
		public string RoleName { get; set; }
	}
}
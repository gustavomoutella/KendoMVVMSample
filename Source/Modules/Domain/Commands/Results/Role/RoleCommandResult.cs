using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Role
{
	public class RoleCommandResult
	{
		public Guid IdRole { get; set; }
		
		public string RoleName { get; set; }
		
		public string Description { get; set; }
		
		public bool Enabled { get; set; }
		
		public ICollection<SelectListUserCommandResult> Users { get; set; }
	}
}
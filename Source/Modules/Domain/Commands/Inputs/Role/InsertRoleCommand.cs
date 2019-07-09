using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role
{
	public class InsertRoleCommand : InputCommand
	{
		public string RoleName { get; set; }
		
		public string Description { get; set; }
		
		public bool Enabled { get; set; }
		
		public ICollection<Guid> Users { get; set; }
	}
}
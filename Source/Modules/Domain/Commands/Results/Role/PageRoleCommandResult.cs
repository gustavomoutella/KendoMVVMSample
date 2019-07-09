using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Role
{
	public class PageRoleCommandResult
	{
		public Guid IdRole { get; set; }
		
		public string RoleName { get; set; }
		
		public string Description { get; set; }
		
		public bool Enabled { get; set; }
	}
}
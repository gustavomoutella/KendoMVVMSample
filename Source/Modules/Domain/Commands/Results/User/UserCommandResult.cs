using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.User
{
	public class UserCommandResult
	{
		public Guid IdUser { get; set; }
		
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		public string Username { get; set; }
		
		public string Password { get; set; }
		
		public string Email { get; set; }
		
		public bool EmailConfirmed { get; set; }
		
		public DateTime? LogonDate { get; set; }
		
		public DateTime? LastActionDate { get; set; }
		
		public DateTime CreationDate { get; set; }
		
		public int InvalidLogonAmount { get; set; }
		
		public bool Enabled { get; set; }
		
		public bool Blocked { get; set; }
		
		public string SecurityKey { get; set; }

        public int? IdPessoa { get; set; }

        public ICollection<SelectListRoleCommandResult> Roles { get; set; }
	}
}
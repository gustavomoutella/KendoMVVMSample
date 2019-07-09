using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role
{
	public class PageRoleCommand : PageCommand
    {
		public string TextToSearch { get; set; }
	}
}
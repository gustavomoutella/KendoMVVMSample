using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.User
{
	public class PageUserCommand : PageCommand
    {
		public string TextToSearch { get; set; }
	}
}
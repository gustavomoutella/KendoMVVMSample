using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization
{
	public class PageAuthorizationCommand : PageCommand
    {
		public string TextToSearch { get; set; }
	}
}
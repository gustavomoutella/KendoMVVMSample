using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature
{
	public class PageFeatureCommand : PageCommand
    {
		public string TextToSearch { get; set; }
	}
}
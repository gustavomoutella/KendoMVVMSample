using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature
{
	public class UpdateFeatureCommand : InsertFeatureCommand
	{
		public Guid IdFeature { get; set; }
	}
}
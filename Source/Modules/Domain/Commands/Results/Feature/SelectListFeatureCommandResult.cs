using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Feature
{
	public class SelectListFeatureCommandResult
	{
		public Guid IdFeature { get; set; }
		
		public string FeatureName { get; set; }
		
		public string RecursiveName { get; set; }
	}
}
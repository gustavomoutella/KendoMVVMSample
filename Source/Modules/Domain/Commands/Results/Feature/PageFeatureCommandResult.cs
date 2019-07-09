using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Feature
{
	public class PageFeatureCommandResult
	{
		public Guid IdFeature { get; set; }
		
		public Guid? IdFeatureParent { get; set; }
		
		public string FeatureName { get; set; }
		
		public string FeatureKey { get; set; }
		
		public string Description { get; set; }
		
		public string RecursiveName { get; set; }
		
		public SelectListFeatureCommandResult Parent { get; set; }
	}
}
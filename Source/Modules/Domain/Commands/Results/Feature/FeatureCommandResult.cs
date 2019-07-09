using System;
using System.Collections.Generic;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Feature
{
	public class FeatureCommandResult
	{
		public Guid IdFeature { get; set; }
		
		public Guid? IdFeatureParent { get; set; }
		
		public string FeatureName { get; set; }
		
		public string FeatureKey { get; set; }
		
		public string Description { get; set; }
		
		public FeatureCommandResult Parent { get; set; }
		
		public ICollection<FeatureCommandResult> Children { get; set; }
	}
}
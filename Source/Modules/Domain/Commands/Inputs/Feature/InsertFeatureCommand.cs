using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature
{
	public class InsertFeatureCommand : InputCommand
	{
		public Guid? IdFeatureParent { get; set; }
		
		public string FeatureName { get; set; }
		
		public string FeatureKey { get; set; }
		
		public string Description { get; set; }

        public string RecursiveName { get; set; }
    }
}
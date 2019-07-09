using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;

namespace Vsol.Api.AppSecurity.Domain.Specs
{
	public static class FeatureSpecs
	{
		public static readonly Expression<Func<FeatureInfo, SelectListFeatureCommandResult>> AsSelectListFeatureCommandResult = x => new SelectListFeatureCommandResult
		{
			IdFeature = x.IdFeature,
			FeatureName = x.FeatureName,
			RecursiveName = x.RecursiveName
		};
		
		public static readonly Expression<Func<FeatureInfo, PageFeatureCommandResult>> AsPageFeatureCommandResult = x => new PageFeatureCommandResult
		{
			IdFeature = x.IdFeature,
			IdFeatureParent = x.IdFeatureParent,
			FeatureName = x.FeatureName,
			FeatureKey = x.FeatureKey,
			Description = x.Description,
			RecursiveName = x.RecursiveName,
			Parent = x.Parent == null ? null : new SelectListFeatureCommandResult
			{
				IdFeature = x.IdFeatureParent.Value, 
				FeatureName = x.Parent.FeatureName
			}
		};
		
		public static readonly Expression<Func<FeatureInfo, FeatureCommandResult>> AsFeatureCommandResult = x => new FeatureCommandResult
		{
			IdFeature = x.IdFeature,
			IdFeatureParent = x.IdFeatureParent,
			FeatureName = x.FeatureName,
			FeatureKey = x.FeatureKey,
			Description = x.Description
		};
		
		public static Expression<Func<FeatureInfo, bool>> TextToSearch(string textToSearch)
		{
			return x => (
				x.FeatureName.Contains(textToSearch) || 
				x.FeatureKey.Contains(textToSearch) || 
				x.Description.Contains(textToSearch)			
			);
		}
	}
}
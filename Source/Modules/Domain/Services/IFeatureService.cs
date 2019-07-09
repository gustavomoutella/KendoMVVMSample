﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Services
{
	public interface IFeatureApplicationService
	{
		Task<FeatureCommandResult> GetByIdAsync(Guid idFeature);
		
		Task<IEnumerable<FeatureCommandResult>> GetAsync();
		
		Task<PaginatedList<PageFeatureCommandResult>> GetPageAsync(PageFeatureCommand command);
		
		Task<IEnumerable<FeatureCommandResult>> GetRecursiveAsync();

        Task<IEnumerable<FeatureCommandResult>> GetByParentAsync(Guid? idFeatureParent);

        Task<IEnumerable<SelectListFeatureCommandResult>> GetSelectListAsync();
	}
}
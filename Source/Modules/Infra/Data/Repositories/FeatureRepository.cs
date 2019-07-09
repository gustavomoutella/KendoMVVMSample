using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Vsol.Api.Shared.Infra.Data.Repositories;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.Shared.Domain;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.AppSecurity.Domain.Specs;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;

namespace Vsol.Api.AppSecurity.Infra.Data.Repositories
{
	public class FeatureRepository : Repository, IFeatureRepository
	{
		private readonly IUnitOfWork _uow;
		private readonly AppSecurityDataContext _context;
		
		public FeatureRepository(IUnitOfWork uow, AppSecurityDataContext context) : base(uow, context)
		{
			_uow = uow;
			_context = context;
		}
		
		public async Task<FeatureCommandResult> GetByIdAsync(Guid idFeature)
		{
			return await _context.Feature.AsNoTracking()
				.Where(x => x.IdFeature == idFeature)
				.Select(FeatureSpecs.AsFeatureCommandResult)
				.SingleOrDefaultAsync();
		}
		
		public async Task<IEnumerable<FeatureCommandResult>> GetAsync()
		{
			return await _context.Feature.AsNoTracking()
				.OrderBy(x => x.FeatureName)
				.Include(x => x.Parent)
				.Select(FeatureSpecs.AsFeatureCommandResult)
				.ToListAsync();
		}
		
		public async Task<PaginatedList<PageFeatureCommandResult>> GetPageAsync(PageFeatureCommand command)
		{
			var source = _context.Feature.AsNoTracking().AsExpandable();
			var outer = PredicateBuilder.New<FeatureInfo>(true);
			
			if (!string.IsNullOrEmpty(command.TextToSearch))
			{
				var inner = PredicateBuilder.New<FeatureInfo>();
				inner = inner.Start(FeatureSpecs.TextToSearch(command.TextToSearch));
				outer = outer.And(inner);
			}
			
			var count = await source.Where(outer).CountAsync();
			var items = await source.Where(outer)
                .Skip(command.SkipNumber)
                .Take(command.PageSize)
				.Include(x => x.Parent)
				.Select(FeatureSpecs.AsPageFeatureCommandResult)
				.ToListAsync();
			
			return new PaginatedList<PageFeatureCommandResult>(items, count, command.PageNumber, command.PageSize);
		}

        public async Task<IEnumerable<FeatureCommandResult>> GetByParentAsync(Guid? idFeatureParent)
        {
            return await _context.Feature.AsNoTracking()
                .Where(x => x.IdFeatureParent == idFeatureParent)
                .OrderBy(x => x.FeatureName)
                .Select(FeatureSpecs.AsFeatureCommandResult)
                .ToListAsync();
        }

        public async Task<IEnumerable<SelectListFeatureCommandResult>> GetSelectListAsync()
		{
			return await _context.Feature.AsNoTracking()
				.OrderBy(x => x.RecursiveName)
				.Select(FeatureSpecs.AsSelectListFeatureCommandResult)
				.ToListAsync();
		}
	}
}
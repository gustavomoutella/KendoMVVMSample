using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Feature;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Shared.Application;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api.AppSecurity.Application.Services
{
	public class FeatureApplicationService : ApplicationService, IFeatureApplicationService
	{
		private readonly IFeatureRepository _repository;
		private readonly FeatureCommandHandler _handler;
		
		public FeatureApplicationService(IUnitOfWork uow, IFeatureRepository repository, FeatureCommandHandler handler) : base(uow)
		{
			this._handler = handler;
			this._repository = repository;
		}
		
		public async Task<FeatureCommandResult> GetByIdAsync(Guid idFeature)
		{
			return await _repository.GetByIdAsync(idFeature);
		}
		
		public async Task<IEnumerable<FeatureCommandResult>> GetAsync()
		{
			return await _repository.GetAsync();
		}
		
		public async Task<PaginatedList<PageFeatureCommandResult>> GetPageAsync(PageFeatureCommand command)
		{
			return await _repository.GetPageAsync(command);
		}
		
		public async Task<IEnumerable<FeatureCommandResult>> GetRecursiveAsync()
		{
			var items = await _repository.GetAsync();

            foreach (var item in items)
            {
                item.Children = items.Where(x => x.IdFeatureParent == item.IdFeature).ToList();
            }

            return items.Where(x => x.IdFeatureParent == null).ToList();
        }

        public async Task<IEnumerable<FeatureCommandResult>> GetByParentAsync(Guid? idFeatureParent)
        {
            return await _repository.GetByParentAsync(idFeatureParent);
        }

        public async Task<IEnumerable<SelectListFeatureCommandResult>> GetSelectListAsync()
		{
			return await _repository.GetSelectListAsync();
		}
	}
}
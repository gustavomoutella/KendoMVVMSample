using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Shared.Application;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api.AppSecurity.Application.Services
{
	public class RoleApplicationService : ApplicationService, IRoleApplicationService
	{
		private readonly IRoleRepository _repository;
		private readonly RoleCommandHandler _handler;
		
		public RoleApplicationService(IUnitOfWork uow, IRoleRepository repository, RoleCommandHandler handler) : base(uow)
		{
			this._handler = handler;
			this._repository = repository;
		}
		
		public async Task<RoleCommandResult> GetByIdAsync(Guid idRole)
		{
			return await _repository.GetByIdAsync(idRole);
		}
		
		public async Task<IEnumerable<RoleCommandResult>> GetAsync()
		{
			return await _repository.GetAsync();
		}
		
		public async Task<PaginatedList<PageRoleCommandResult>> GetPageAsync(PageRoleCommand command)
		{
			return await _repository.GetPageAsync(command);
		}
		
		public async Task<IEnumerable<SelectListRoleCommandResult>> GetSelectListAsync()
		{
			return await _repository.GetSelectListAsync();
		}
	}
}
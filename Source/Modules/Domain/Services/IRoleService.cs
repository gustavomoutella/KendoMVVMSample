using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Services
{
	public interface IRoleApplicationService
	{
		Task<RoleCommandResult> GetByIdAsync(Guid idRole);
		
		Task<IEnumerable<RoleCommandResult>> GetAsync();
		
		Task<PaginatedList<PageRoleCommandResult>> GetPageAsync(PageRoleCommand command);
		
		Task<IEnumerable<SelectListRoleCommandResult>> GetSelectListAsync();
	}
}
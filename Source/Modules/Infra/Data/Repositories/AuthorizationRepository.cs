using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Vsol.Api.Shared.Infra.Data.Repositories;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization;
using Vsol.Api.AppSecurity.Domain.Specs;
using Vsol.Api.Shared.Domain;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;

namespace Vsol.Api.AppSecurity.Infra.Data.Repositories
{
	public class AuthorizationRepository : Repository, IAuthorizationRepository
    {
		private readonly IUnitOfWork _uow;
		private readonly AppSecurityDataContext _context;
		
		public AuthorizationRepository(IUnitOfWork uow, AppSecurityDataContext context) : base(uow, context)
		{
			_uow = uow;
			_context = context;
		}
		
		public async Task<AuthorizationCommandResult> GetByIdAsync(Guid idAuthorization)
		{
			return await _context.Authorization.AsNoTracking()
				.Where(x => x.IdAuthorization == idAuthorization)
				.Select(AuthorizationSpecs.AsAuthorizationCommandResult)
				.SingleOrDefaultAsync();
		}
		
		public async Task<IEnumerable<AuthorizationCommandResult>> GetAsync()
		{
			return await _context.Authorization.AsNoTracking()
				.OrderBy(x => x.Authorized)
				.Include(x => x.Feature)
				.Include(x => x.Role)
				.Select(AuthorizationSpecs.AsAuthorizationCommandResult)
				.ToListAsync();
		}
		
		public async Task<PaginatedList<PageAuthorizationCommandResult>> GetPageAsync(PageAuthorizationCommand command)
		{
			var source = _context.Authorization.AsNoTracking().AsExpandable();
			var outer = PredicateBuilder.New<AuthorizationInfo>(true);
			
			if (!string.IsNullOrEmpty(command.TextToSearch))
			{
				var inner = PredicateBuilder.New<AuthorizationInfo>();
				inner = inner.Start(AuthorizationSpecs.TextToSearch(command.TextToSearch));
				outer = outer.And(inner);
			}
			
			var count = await source.Where(outer).CountAsync();
			var items = await source.Where(outer)
                .Skip(command.SkipNumber)
                .Take(command.PageSize)
				.Include(x => x.Feature)
				.Include(x => x.Role)
				.Select(AuthorizationSpecs.AsPageAuthorizationCommandResult)
				.ToListAsync();
			
			return new PaginatedList<PageAuthorizationCommandResult>(items, count, command.PageNumber, command.PageSize);
		}

        public async Task<IList<SelectListAuthorizationCommandResult>> GetPermissionsByRolesAsync(IList<Guid> idRoles)
        {
            return await _context.Authorization.AsNoTracking()
                .Include(x => x.Feature)
                .Where(x =>
                    x.Authorized == true &&
                    !string.IsNullOrEmpty(x.Feature.FeatureKey) &&
                    idRoles.Contains(x.IdRole))
                .Select(AuthorizationSpecs.AsSelectListAuthorizationCommandResult)
                .ToListAsync();
        }
    }
}
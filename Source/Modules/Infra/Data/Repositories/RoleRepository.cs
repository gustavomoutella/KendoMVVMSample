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
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;
using Vsol.Api.Shared.Domain;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Specs;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;

namespace Vsol.Api.AppSecurity.Infra.Data.Repositories
{
	public class RoleRepository : Repository, IRoleRepository
	{
		private readonly IUnitOfWork _uow;
		private readonly AppSecurityDataContext _context;
		
		public RoleRepository(IUnitOfWork uow, AppSecurityDataContext context) : base(uow, context)
		{
			_uow = uow;
			_context = context;
		}
		
		public async Task<RoleCommandResult> GetByIdAsync(Guid idRole)
		{
			return await _context.Role.AsNoTracking()
				.Where(x => x.IdRole == idRole)
                .Include(x => x.UsersInRoles)
                .Select(RoleSpecs.AsRoleCommandResult)
				.SingleOrDefaultAsync();
		}
		
		public async Task<IEnumerable<RoleCommandResult>> GetAsync()
		{
			return await _context.Role.AsNoTracking()
				.OrderBy(x => x.RoleName)
                .Include(x => x.UsersInRoles)
                .Select(RoleSpecs.AsRoleCommandResult)
				.ToListAsync();
		}
		
		public async Task<PaginatedList<PageRoleCommandResult>> GetPageAsync(PageRoleCommand command)
		{
			var source = _context.Role.AsNoTracking().AsExpandable();
			var outer = PredicateBuilder.New<RoleInfo>(true);
			
			if (!string.IsNullOrEmpty(command.TextToSearch))
			{
				var inner = PredicateBuilder.New<RoleInfo>();
				inner = inner.Start(RoleSpecs.TextToSearch(command.TextToSearch));
				outer = outer.And(inner);
			}
			
			var count = await source.Where(outer).CountAsync();
			var items = await source.Where(outer)
                .Skip(command.SkipNumber)
                .Take(command.PageSize)
				.Select(RoleSpecs.AsPageRoleCommandResult)
				.ToListAsync();
			
			return new PaginatedList<PageRoleCommandResult>(items, count, command.PageNumber, command.PageSize);
		}
		
		public async Task<IEnumerable<SelectListRoleCommandResult>> GetSelectListAsync()
		{
			return await _context.Role.AsNoTracking()
				.OrderBy(x => x.RoleName)
				.Select(RoleSpecs.AsSelectListRoleCommandResult)
				.ToListAsync();
		}
		
        public async Task<IList<Guid>> GetByUser(Guid idUser)
        {
            var items = await _uow.Connection.QueryAsync<Guid>(
                "select idRole from AppSecurity.UserInRole where idUser = @idUser",
                new { idUser },
                _uow.Transaction);

            return items.ToList();
        }
    }
}
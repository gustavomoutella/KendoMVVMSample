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
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;
using Vsol.Api.AppSecurity.Domain.Specs;
using Vsol.Api.Shared.Domain;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;

namespace Vsol.Api.AppSecurity.Infra.Data.Repositories
{
	public class UserRepository : Repository, IUserRepository
	{
		private readonly IUnitOfWork _uow;
		private readonly AppSecurityDataContext _context;
		
		public UserRepository(IUnitOfWork uow, AppSecurityDataContext context) : base(uow, context)
		{
			_uow = uow;
			_context = context;
		}
		
		public async Task<UserCommandResult> GetByIdAsync(Guid idUser)
		{
			return await _context.User.AsNoTracking()
				.Where(x => x.IdUser == idUser)
                .Include(x => x.UsersInRoles)
                .Select(UserSpecs.AsUserCommandResult)
				.SingleOrDefaultAsync();
		}

        public async Task<SelectListUserCommandResult> GetBasicByIdAsync(Guid idUser)
        {
            return await _context.User.AsNoTracking()
                .Where(x => x.IdUser == idUser)
                .Select(UserSpecs.AsSelectListUserCommandResult)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserCommandResult>> GetAsync()
		{
			return await _context.User.AsNoTracking()
				.OrderBy(x => x.FirstName)
                .Include(x => x.UsersInRoles)
                .Select(UserSpecs.AsUserCommandResult)
				.ToListAsync();
		}
		
		public async Task<PaginatedList<PageUserCommandResult>> GetPageAsync(PageUserCommand command)
		{
			var source = _context.User.AsNoTracking().AsExpandable();
			var outer = PredicateBuilder.New<UserInfo>(true);
			
			if (!string.IsNullOrEmpty(command.TextToSearch))
			{
				var inner = PredicateBuilder.New<UserInfo>();
				inner = inner.Start(UserSpecs.TextToSearch(command.TextToSearch));
				outer = outer.And(inner);
			}
			
			var count = await source.Where(outer).CountAsync();
			var items = await source.Where(outer)
                .Skip(command.SkipNumber)
                .Take(command.PageSize)
				.Select(UserSpecs.AsPageUserCommandResult)
				.ToListAsync();
			
			return new PaginatedList<PageUserCommandResult>(items, count, command.PageNumber, command.PageSize);
		}
		
		public async Task<IEnumerable<SelectListUserCommandResult>> GetSelectListAsync()
		{
			return await _context.User.AsNoTracking()
				.OrderBy(x => x.FirstName)
				.Select(UserSpecs.AsSelectListUserCommandResult)
				.ToListAsync();
		}
		
        public async Task<UserInfo> GetByUserName(string username)
        {
            return await _context.User.AsNoTracking()
                .Where(x => x.Username == username)
                .SingleOrDefaultAsync();
        }
    }
}
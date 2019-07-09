using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Shared.Application;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api.AppSecurity.Application.Services
{
	public class UserApplicationService : ApplicationService, IUserApplicationService
	{
		private readonly IUserRepository _repository;
		private readonly UserCommandHandler _handler;
		
		public UserApplicationService(IUnitOfWork uow, IUserRepository repository, UserCommandHandler handler) : base(uow)
		{
			this._handler = handler;
			this._repository = repository;
		}
		
		public async Task<UserCommandResult> GetByIdAsync(Guid idUser)
		{
			return await _repository.GetByIdAsync(idUser);
		}

        public async Task<SelectListUserCommandResult> GetBasicByIdAsync(Guid idUser)
        {
            return await _repository.GetBasicByIdAsync(idUser);
        }

        public async Task<IEnumerable<UserCommandResult>> GetAsync()
		{
			return await _repository.GetAsync();
		}
		
		public async Task<PaginatedList<PageUserCommandResult>> GetPageAsync(PageUserCommand command)
		{
			return await _repository.GetPageAsync(command);
		}
		
		public async Task<IEnumerable<SelectListUserCommandResult>> GetSelectListAsync()
		{
			return await _repository.GetSelectListAsync();
		}

        public async Task<NotificationResult> IsValidUsernameAndPasswordAsync(string username, string password)
        {
            BeginTransaction();
            var result = await _handler.CheckUserAsync(username, null, password);
            return Commit(result);
        }

        public async Task<NotificationResult> IsValidUsernameAndTokenAsync(string username, Guid idUser)
        {
            BeginTransaction();
            var result = await _handler.CheckUserAsync(username, idUser, null);
            return Commit(result);
        }
    }
}
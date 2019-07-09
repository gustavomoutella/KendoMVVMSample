using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.Shared.Application;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api.AppSecurity.Application.Services
{
    public class AuthorizationApplicationService : ApplicationService, IAuthorizationApplicationService
    {
        private readonly IAuthorizationRepository _repository;
        private readonly AuthorizationCommandHandler _handler;

        private readonly IRoleRepository _roleRepository;

        private const string cacheRegionName = "Security_Authorizations";
        private const string cacheKeyName = "User";

        public AuthorizationApplicationService(IUnitOfWork uow, IAuthorizationRepository repository, AuthorizationCommandHandler handler, IRoleRepository roleRepository) : base(uow)
        {
            this._handler = handler;
            this._repository = repository;

            this._roleRepository = roleRepository;
        }

        public async Task<AuthorizationCommandResult> GetByIdAsync(Guid idAuthorization)
        {
            return await _repository.GetByIdAsync(idAuthorization);
        }

        public async Task<IEnumerable<AuthorizationCommandResult>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<PaginatedList<PageAuthorizationCommandResult>> GetPageAsync(PageAuthorizationCommand command)
        {
            return await _repository.GetPageAsync(command);
        }

        public async Task<bool> CheckPermissionAsync(Guid idUser, string featureKey)
        {
            var items = await GetPermissionsByUserAsync(idUser);
            return items.Any(x => x.FeatureKey.ToLower() == featureKey.ToLower());
        }

        public async Task<IList<SelectListAuthorizationCommandResult>> GetPermissionsByUserAsync(Guid idUser)
        {
            var idRoles = await _roleRepository.GetByUser(idUser);
            var items = await _repository.GetPermissionsByRolesAsync(idRoles);

            return items;
        }
    }
}
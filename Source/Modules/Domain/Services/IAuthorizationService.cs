using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Services
{
    public interface IAuthorizationApplicationService
    {
        Task<AuthorizationCommandResult> GetByIdAsync(Guid idAuthorization);

        Task<IEnumerable<AuthorizationCommandResult>> GetAsync();

        Task<PaginatedList<PageAuthorizationCommandResult>> GetPageAsync(PageAuthorizationCommand command);

        Task<bool> CheckPermissionAsync(Guid idUser, string featureKey);

        Task<IList<SelectListAuthorizationCommandResult>> GetPermissionsByUserAsync(Guid idUser);
    }
}
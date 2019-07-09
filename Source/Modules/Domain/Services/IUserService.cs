using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Services
{
    public interface IUserApplicationService
    {
        Task<UserCommandResult> GetByIdAsync(Guid idUser);

        Task<SelectListUserCommandResult> GetBasicByIdAsync(Guid idUser);

        Task<IEnumerable<UserCommandResult>> GetAsync();

        Task<PaginatedList<PageUserCommandResult>> GetPageAsync(PageUserCommand command);

        Task<IEnumerable<SelectListUserCommandResult>> GetSelectListAsync();

        Task<NotificationResult> IsValidUsernameAndPasswordAsync(string username, string password);

        Task<NotificationResult> IsValidUsernameAndTokenAsync(string username, Guid idUser);
    }
}
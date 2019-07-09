using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Authorization;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Handlers
{
    public class AuthorizationCommandHandler
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationCommandHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

    }
}
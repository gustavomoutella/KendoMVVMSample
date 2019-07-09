using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Repositories;

namespace Vsol.Api.AppSecurity.Domain.Commands.Handlers
{
    public class RoleCommandHandler
    {
        private readonly IRoleRepository _roleRepository;

        public RoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Services;

namespace Vsol.Api
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        private readonly IAuthorizationApplicationService authorizationApp;

        public PermissionHandler(IAuthorizationApplicationService authorizationApp)
        {
            this.authorizationApp = authorizationApp;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identities.Any(i => i.IsAuthenticated))
            {
                // no user authorizedd. Alternatively call context.Fail() to ensure a failure 
                // as another handler for this requirement may succeed
                //context.Fail();

                Guid idUser = new Guid(idUsr);
                bool hasPermission = await authorizationApp.CheckPermissionAsync(idUser, requirement.Permission);

                if (hasPermission)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

            }
            else
            {
                // User identifier
                Guid idUser = new Guid(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                bool hasPermission = await authorizationApp.CheckPermissionAsync(idUser, requirement.Permission);

                if (hasPermission)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
        }

        public const string idUsr = "4ED0F619-5790-4297-AA5F-F7E7FA20A478";

    }
}

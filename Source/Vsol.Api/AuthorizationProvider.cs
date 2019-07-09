using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Application.Services;
using Vsol.Api.AppSecurity.Domain.Commands.Handlers;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.AppSecurity.Domain.Services;
using Vsol.Api.AppSecurity.Infra.Data.Contexts;
using Vsol.Api.AppSecurity.Infra.Data.Repositories;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;
using static AspNet.Security.OpenIdConnect.Primitives.OpenIdConnectConstants;

namespace Vsol.Api
{
    public class AuthorizationProvider : OpenIdConnectServerProvider
    {
        public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    description: "Tipo de autenticaçao nao suportado pelo servidor.");

                return Task.FromResult(0);
            }

            context.Skip();
            return Task.FromResult(0);
        }

        public override async Task HandleTokenRequest(HandleTokenRequestContext context)
        {
            NotificationResult result = null;
            IUserApplicationService userApp = GetUserApp();

            if (context.Request.IsPasswordGrantType())
            {
                result = await userApp.IsValidUsernameAndPasswordAsync(context.Request.Username, context.Request.Password);
            }
            else if (context.Request.IsRefreshTokenGrantType())
            {
                Guid idUser = new Guid(context.Ticket.Principal.GetClaim(ClaimTypes.NameIdentifier));
                string username = context.Ticket.Principal.GetClaim(ClaimTypes.Name);

                result = await userApp.IsValidUsernameAndTokenAsync(username, idUser);
            }

            if (result.IsValid && result.Data == null && result.Messages.Any())
                result.AddError(result.Messages.First());

            if (!result.IsValid)
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.InvalidGrant,
                    description: result.Errors.FirstOrDefault()
                );
            }
            else
            {
                var identity = new ClaimsIdentity(OpenIdConnectServerDefaults.AuthenticationScheme);
                var user = result.Data as UserInfo;

                identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.IdUser.ToString());
                identity.AddClaim(ClaimTypes.NameIdentifier, user.IdUser.ToString(), Destinations.AccessToken);
                identity.AddClaim(ClaimTypes.Name, user.Username, Destinations.AccessToken);
                identity.AddClaim(ClaimTypes.GivenName, user.FirstName, Destinations.AccessToken, Destinations.IdentityToken);
                identity.AddClaim(ClaimTypes.Email, user.Email, Destinations.AccessToken, Destinations.IdentityToken);

                var ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties(),
                    OpenIdConnectServerDefaults.AuthenticationScheme
                );

                ticket.SetScopes(
                    Scopes.OpenId,
                    Scopes.OfflineAccess
                );

                context.Validate(ticket);
            }
        }

        private IUserApplicationService GetUserApp()
        {
            IUnitOfWork _uow = new UnitOfWork();
            AppSecurityDataContext _context = new AppSecurityDataContext(_uow);
            IUserRepository _repository = new UserRepository(_uow, _context);
            UserCommandHandler _handler = new UserCommandHandler(_repository);
            IUserApplicationService userApp = new UserApplicationService(_uow, _repository, _handler);
            return userApp;
        }
    }
}

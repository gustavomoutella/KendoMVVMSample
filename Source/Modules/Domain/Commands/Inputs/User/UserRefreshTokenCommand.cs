using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.User
{
    public class UserRefreshTokenCommand
    {
        public string Username { get; set; }

        public string Refresh_token { get; set; }
    }
}

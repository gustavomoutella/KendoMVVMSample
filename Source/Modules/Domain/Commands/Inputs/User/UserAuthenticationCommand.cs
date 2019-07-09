using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.User
{
    public class UserAuthenticationCommand
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}

using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.User
{
    public class UserTokenCommandResult
    {
        public string Token_type { get; set; }

        public string Access_token { get; set; }

        public int Expires_in { get; set; }

        public string Refresh_token { get; set; }

        public string Id_token { get; set; }

        public string Error { get; set; }

        public string Error_description { get; set; }
    }
}

using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.Authorization
{
    public class SelectListAuthorizationCommandResult
    {
        public Guid IdAuthorization { get; set; }

        public string FeatureKey { get; set; }

        public bool Authorized { get; set; }

        public string RecursiveName { get; set; }
    }
}
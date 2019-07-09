using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Results.User
{
    public class SelectListUserCommandResult
    {
        public Guid IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public int? IdPessoa { get; set; }

        public string Fullname { get { return FirstName + " " + LastName; } }
    }
}
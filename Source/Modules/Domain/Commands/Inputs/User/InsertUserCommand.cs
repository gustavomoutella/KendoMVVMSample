using System;
using System.Collections.Generic;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.User
{
	public class InsertUserCommand
	{
        private bool _emailConfirmed = false;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get { return _emailConfirmed; } }

        public DateTime CreationDate { get { return DateTime.Now; } }

        public int InvalidLoginAmount { get { return 0; } }

        public bool Enabled { get { return true; } }
        public int? IdPessoa { get; set; }
        public ICollection<Guid> Roles { get; set; }

        // Methods
        public void SetEmailConfirmed()
        {
            _emailConfirmed = true;
        }
    }
}
using System;

namespace Vsol.Api.AppSecurity.Domain.Commands.Inputs.User
{
	public class UpdateUserCommand : InsertUserCommand
	{
		public Guid IdUser { get; set; }
	}
}
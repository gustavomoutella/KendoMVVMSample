using System;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Entities
{
	public class UserInRoleInfo : EntityInfo<UserInRoleInfo>
	{
        public UserInRoleInfo() { }
		
		public Guid IdRole { get; private set; }
		
		public Guid IdUser { get; private set; }
		
		#region Navigation properties
		
		public virtual RoleInfo Role { get; set; }
		
		public virtual UserInfo User { get; set; }
		
		#endregion
	}
}
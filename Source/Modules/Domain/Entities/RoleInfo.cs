using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Role;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Entities
{
	public class RoleInfo : EntityInfo<RoleInfo>
	{
		public RoleInfo() { }
		
		public RoleInfo(InsertRoleCommand command)
		{
			Map(command, this);
			IdRole = Guid.NewGuid();
            InitCollections();
		}
		
		public RoleInfo(UpdateRoleCommand command)
		{
			Map(command, this);
            InitCollections();
		}

        private void InitCollections()
        {
            Authorizations = new List<AuthorizationInfo>();
            UsersInRoles = new List<UserInRoleInfo>();
        }

        public Guid IdRole { get; private set; }
		
		public string RoleName { get; private set; }
		
		public string Description { get; private set; }
		
		public bool Enabled { get; private set; }
		
		#region Navigation properties
		
		public virtual ICollection<UserInRoleInfo> UsersInRoles { get; set; }
		
		public virtual ICollection<AuthorizationInfo> Authorizations { get; set; }
		
		#endregion
	}
}
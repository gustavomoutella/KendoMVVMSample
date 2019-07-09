using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;

namespace Vsol.Api.AppSecurity.Domain.Specs
{
	public static class RoleSpecs
	{
		public static readonly Expression<Func<RoleInfo, SelectListRoleCommandResult>> AsSelectListRoleCommandResult = x => new SelectListRoleCommandResult
		{
			IdRole = x.IdRole,
			RoleName = x.RoleName
		};
		
		public static readonly Expression<Func<RoleInfo, PageRoleCommandResult>> AsPageRoleCommandResult = x => new PageRoleCommandResult
		{
			IdRole = x.IdRole,
			RoleName = x.RoleName,
			Description = x.Description,
			Enabled = x.Enabled
		};
		
		public static readonly Expression<Func<RoleInfo, RoleCommandResult>> AsRoleCommandResult = x => new RoleCommandResult
		{
			IdRole = x.IdRole,
			RoleName = x.RoleName,
			Description = x.Description,
			Enabled = x.Enabled,
			Users = x.UsersInRoles.Select(
				y => new SelectListUserCommandResult
				{
					IdUser = y.IdUser,
					FirstName = y.User.FirstName
				}).ToList()
		};
		
		public static Expression<Func<RoleInfo, bool>> TextToSearch(string textToSearch)
		{
			return x => (
				x.RoleName.Contains(textToSearch) || 
				x.Description.Contains(textToSearch)			
			);
		}
	}
}
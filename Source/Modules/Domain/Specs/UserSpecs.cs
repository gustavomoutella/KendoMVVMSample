using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Commands.Results.User;
using Vsol.Api.AppSecurity.Domain.Commands.Results.Role;

namespace Vsol.Api.AppSecurity.Domain.Specs
{
    public static class UserSpecs
    {
        public static readonly Expression<Func<UserInfo, SelectListUserCommandResult>> AsSelectListUserCommandResult = x => new SelectListUserCommandResult
        {
            IdUser = x.IdUser,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Username = x.Username,
            IdPessoa = x.IdPessoa
        };

        public static readonly Expression<Func<UserInfo, PageUserCommandResult>> AsPageUserCommandResult = x => new PageUserCommandResult
        {
            IdUser = x.IdUser,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Username = x.Username,
            Password = x.Password,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            LogonDate = x.LogonDate,
            LastActionDate = x.LastActionDate,
            CreationDate = x.CreationDate,
            InvalidLogonAmount = x.InvalidLogonAmount,
            Enabled = x.Enabled,
            Blocked = x.Blocked,
            SecurityKey = x.SecurityKey,
            IdPessoa = x.IdPessoa
        };

        public static readonly Expression<Func<UserInfo, UserCommandResult>> AsUserCommandResult = x => new UserCommandResult
        {
            IdUser = x.IdUser,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Username = x.Username,
            Password = x.Password,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            LogonDate = x.LogonDate,
            LastActionDate = x.LastActionDate,
            CreationDate = x.CreationDate,
            InvalidLogonAmount = x.InvalidLogonAmount,
            Enabled = x.Enabled,
            Blocked = x.Blocked,
            SecurityKey = x.SecurityKey,
            IdPessoa = x.IdPessoa,
            Roles = x.UsersInRoles.Select(
                y => new SelectListRoleCommandResult
                {
                    IdRole = y.IdRole,
                    RoleName = y.Role.RoleName
                }).ToList()

        };

        public static Expression<Func<UserInfo, bool>> TextToSearch(string textToSearch)
        {
            return x => (
                x.FirstName.Contains(textToSearch) ||
                x.LastName.Contains(textToSearch) ||
                x.Username.Contains(textToSearch) ||
                x.Password.Contains(textToSearch) ||
                x.Email.Contains(textToSearch) ||
                x.SecurityKey.Contains(textToSearch)
            );
        }
    }
}
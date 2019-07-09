using System;
using System.Collections.Generic;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Entities
{
    public class UserInfo : EntityInfo<UserInfo>
    {
        public UserInfo() { }

        public UserInfo(InsertUserCommand command)
        {
            Map(command, this);
            IdUser = Guid.NewGuid();
            InitCollections();
        }

        public UserInfo(UpdateUserCommand command)
        {
            Map(command, this);
            InitCollections();
        }

        private void InitCollections()
        {
            UsersInRoles = new List<UserInRoleInfo>();
        }

        public Guid IdUser { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public DateTime? LogonDate { get; private set; }

        public DateTime? LastActionDate { get; private set; }

        public DateTime CreationDate { get; private set; }

        public int InvalidLogonAmount { get; private set; }

        public bool Enabled { get; private set; }

        public bool Blocked { get; private set; }

        public string SecurityKey { get; private set; }

        public int? IdPessoa { get; set; }

        #region Navigation properties

        public virtual ICollection<UserInRoleInfo> UsersInRoles { get; set; }

        #endregion

        #region Methods

        public void ValidLogon()
        {
            LogonDate = DateTime.Now;
            LastActionDate = DateTime.Now;
            InvalidLogonAmount = 0;
        }

        public void InvalidLogon()
        {
            InvalidLogonAmount += 1;

            if (InvalidLogonAmount >= 5)
                Blocked = true;
        }

        public void SetPassword(string encrypted)
        {
            Password = encrypted;
        }

        public void SetEmailConfirmed()
        {
            this.EmailConfirmed = true;
        }

        #endregion
    }
}
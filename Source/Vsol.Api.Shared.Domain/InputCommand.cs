using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.Shared.Domain
{
    public class InputCommand
    {
        public Guid? IdUser { get; set; }

        public void SetIdUser(Guid? idUser)
        {
            this.IdUser = IdUser;
        }
    }
}

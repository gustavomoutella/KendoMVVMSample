using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.VSolTables.Domain.Commands.Inputs
{
    public class UpdateProductCommand : InsertProductCommand
    {
        public Guid Id { get; set; }
    }
}

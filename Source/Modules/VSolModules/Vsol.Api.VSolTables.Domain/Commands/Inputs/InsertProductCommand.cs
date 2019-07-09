using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.VSolTables.Domain.Commands.Inputs
{
    public class InsertProductCommand
    {
        public string ProductName { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
    }
}

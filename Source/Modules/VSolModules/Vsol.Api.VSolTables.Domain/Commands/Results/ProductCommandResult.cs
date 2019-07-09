using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.VSolTables.Domain.Commands.Results
{
    public class ProductCommandResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
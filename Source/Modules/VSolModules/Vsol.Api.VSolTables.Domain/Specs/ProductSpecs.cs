using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using Vsol.Api.VSolTables.Domain.Commands.Results;
using Vsol.Api.VSolTables.Domain.Entities;

namespace Vsol.Api.VSolTables.Domain.Specs
{
    public static class ProductSpecs
    {
        public static readonly Expression<Func<ProductInfo, ProductCommandResult>> AsProductCommandResult = x => new ProductCommandResult
        {
            Discontinued = x.Discontinued,
            Id = x.Id,
            ProductName = x.ProductName,
            UnitPrice = x.UnitPrice,
            UnitsInStock = x.UnitsInStock
        };

        public static Expression<Func<ProductInfo, bool>> TextToSearch(string textToSearch)
        {
            return x => (
                x.ProductName.Contains(textToSearch)
            );
        }
    }
}
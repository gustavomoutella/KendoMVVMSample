using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;

namespace Vsol.Api.VSolTables.Domain.Entities
{
    public class ProductInfo : EntityInfo<ProductInfo>
    {
        public ProductInfo() { }

        public ProductInfo(InsertProductCommand command)
        {
            Map(command, this);
        }

        public ProductInfo(UpdateProductCommand command)
        {
            Map(command, this);
        }

        public Guid Id { get; private set; }

        public string ProductName { get; private set; }

        public decimal? UnitPrice { get; private set; }

        public decimal? UnitsInStock { get; private set; }

        public bool Discontinued { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vsol.Api.Shared.Domain;
using Vsol.Api.VSolTables.Domain.Commands.Results;
using Vsol.Api.VSolTables.Domain.Entities;

namespace Vsol.Api.VSolTables.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ProductCommandResult> GetByIdAsync(Guid idProduct);

        Task<IEnumerable<ProductCommandResult>> GetAsync();

        Task<NotificationResult> InsertAsync(ProductInfo item);

        Task<NotificationResult> UpdateAsync(ProductInfo item);

        Task<NotificationResult> DeleteAsync(Guid id);
    }
}

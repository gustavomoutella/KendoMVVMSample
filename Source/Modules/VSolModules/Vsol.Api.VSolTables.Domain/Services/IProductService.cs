using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vsol.Api.Shared.Domain;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;
using Vsol.Api.VSolTables.Domain.Commands.Results;

namespace Vsol.Api.VSolTables.Domain.Services
{
    public interface IProductApplicationService
    {
        Task<ProductCommandResult> GetByIdAsync(Guid idProduct);

        Task<IEnumerable<ProductCommandResult>> GetAsync();

        Task<NotificationResult> InsertAsync(InsertProductCommand item);

        Task<NotificationResult> UpdateAsync(UpdateProductCommand item);

        Task<NotificationResult> DeleteAsync(Guid id);
    }
}

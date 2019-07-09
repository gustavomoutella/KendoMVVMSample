using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vsol.Api.Shared.Application;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.VSolTables.Domain.Commands.Handlers;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;
using Vsol.Api.VSolTables.Domain.Commands.Results;
using Vsol.Api.VSolTables.Domain.Repositories;
using Vsol.Api.VSolTables.Domain.Services;

namespace Vsol.Api.VSolTables.Application.Services
{
    public class ProductApplicationService : ApplicationService, IProductApplicationService
    {
        private readonly IProductRepository _repository;
        private readonly ProductCommandHandler _handler;

        public ProductApplicationService(IUnitOfWork uow, IProductRepository repository, ProductCommandHandler handler) : base(uow)
        {
            this._handler = handler;
            this._repository = repository;
        }

        public async Task<ProductCommandResult> GetByIdAsync(Guid idProduct)
        {
            return await _repository.GetByIdAsync(idProduct);
        }

        public async Task<IEnumerable<ProductCommandResult>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<NotificationResult> InsertAsync(InsertProductCommand command)
        {
            BeginTransaction();
            var result = await _handler.InsertAsync(command);
            return Commit(result);
        }

        public async Task<NotificationResult> UpdateAsync(UpdateProductCommand command)
        {
            BeginTransaction();
            var result = await _handler.UpdateAsync(command);
            return Commit(result);
        }

        public async Task<NotificationResult> DeleteAsync(Guid id)
        {
            BeginTransaction();
            var result = await _handler.DeleteByIdAsync(id);
            return Commit(result);
        }
    }
}

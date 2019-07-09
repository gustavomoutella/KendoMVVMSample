using System;
using System.Threading.Tasks;
using Vsol.Api.Shared.Domain;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;
using Vsol.Api.VSolTables.Domain.Entities;
using Vsol.Api.VSolTables.Domain.Repositories;

namespace Vsol.Api.VSolTables.Domain.Commands.Handlers
{
    public class ProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public ProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationResult> InsertAsync(InsertProductCommand command)
        {
            var result = new NotificationResult();

            // Passo 1 - Construindo o objeto Product.
            var item = new ProductInfo(command);

            // Passo 2 - Verificando se o escopo do Product é válido.
            if (!result.IsValid)
                return result;

            // Passo 3 - Inserindo o objeto Product.
            result.Add(await _repository.InsertAsync(item));

            // Passo 4 - Em caso de sucesso, retornando o identificador inserido.
            if (result.IsValid)
            {
                result.Data = item.Id;
                result.AddMessage("Sucesso");
            }
            else
                result.AddError("Erro");

            return result;
        }

        public async Task<NotificationResult> UpdateAsync(UpdateProductCommand command)
        {
            var result = new NotificationResult();

            // Passo 1 - Construindo o objeto Product.
            var item = new ProductInfo(command);

            // Passo 2 - Verificando se o escopo do Product é válido.
            if (!result.IsValid)
                return result;

            // Passo 3 - Atualizando o objeto Product.
            result.Add(await _repository.UpdateAsync(item));

            // Passo 4 - Em caso de sucesso, retornando o identificador atualizado.
            if (result.IsValid)
            {
                result.Data = item.Id;
                result.AddMessage("Sucesso");
            }
            else
                result.AddError("Erro");

            return result;
        }

        public async Task<NotificationResult> DeleteByIdAsync(Guid id)
        {
            var result = new NotificationResult();

            // Passo 1 - Excluindo o registro pela(s) chave(s).
            result.Add(await _repository.DeleteAsync(id));

            // Passo 2 - Adicionando as mensagens de retorno.
            if (result.IsValid)
                result.AddMessage("Sucesso");
            else
                result.AddError("Erro");

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Vsol.Api.Shared.Infra.Data.Repositories;
using Vsol.Api.VSolTables.Domain.Repositories;
using Vsol.Api.Shared.Infra.Data.Transactions;
using Vsol.Api.Shared.Domain;
using Vsol.Api.VSolTables.Domain.Commands.Results;
using Vsol.Api.VSolTables.Domain.Specs;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;
using Vsol.Api.VSolTables.Domain.Entities;
using Vsol.Api.VSolTables.Infra.Data.Contexts;

namespace Vsol.Api.VSolTables.Infra.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly VSolTablesDataContext _context;

        public ProductRepository(IUnitOfWork uow, VSolTablesDataContext context) : base(uow, context)
        {
            _uow = uow;
            _context = context;
        }

        public async Task<ProductCommandResult> GetByIdAsync(Guid idProduct)
        {
            return await _context.Product.AsNoTracking()
                .Where(x => x.Id == idProduct)
                .Select(ProductSpecs.AsProductCommandResult)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductCommandResult>> GetAsync()
        {
            return await _context.Product.AsNoTracking()
                .OrderBy(x => x.ProductName)
                .Select(ProductSpecs.AsProductCommandResult)
                .ToListAsync();
        }

        public async Task<NotificationResult> InsertAsync(ProductInfo item)
        {
            var result = new NotificationResult();

            try
            {
                _context.Product.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }

        public async Task<NotificationResult> UpdateAsync(ProductInfo item)
        {
            var result = new NotificationResult();

            try
            {
                _context.Product.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }

        public async Task<NotificationResult> DeleteAsync(Guid id)
        {
            var result = new NotificationResult();

            try
            {
                var entity = _context.Product
                                .Where(e => e.Id == id)
                                .FirstOrDefault();

                _context.Product.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }
    }
}

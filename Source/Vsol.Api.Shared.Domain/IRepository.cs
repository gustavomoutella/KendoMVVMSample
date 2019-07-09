using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vsol.Api.Shared.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        IList<TEntity> PageAll(int skip, int take);

        Task<List<TEntity>> PageAllAsync(int skip, int take);

        Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take);

        TEntity FindById(object id);

        Task<TEntity> FindByIdAsync(object id);

        Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}

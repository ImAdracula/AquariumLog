using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Base
{
    public interface IBulkDeleteRepository<TEntity, in TId> : IDeleteRepository<TEntity, TId>
    {
        void DeleteAll(IEnumerable<TEntity> entities);
        Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
    }
}
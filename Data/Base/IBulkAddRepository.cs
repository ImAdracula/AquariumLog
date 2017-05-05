using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Base
{
    public interface IBulkAddRepository<TEntity, in TId> : IAddRepository<TEntity, TId>
    {
        void AddAll(IEnumerable<TEntity> entities);
        Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
    }
}
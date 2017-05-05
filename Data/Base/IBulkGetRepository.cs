using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Base
{
    public interface IBulkGetRepository<TEntity, in TId> : IGetRepository<TEntity, TId>
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
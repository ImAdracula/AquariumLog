using System.Threading;
using System.Threading.Tasks;

namespace Data.Base
{
    public interface IAddRepository<TEntity, in TId> : IGetRepository<TEntity, TId>
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
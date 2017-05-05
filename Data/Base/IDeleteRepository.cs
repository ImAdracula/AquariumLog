using System.Threading;
using System.Threading.Tasks;

namespace Data.Base
{
    public interface IDeleteRepository<TEntity, in TId> : IGetRepository<TEntity, TId>
    {
        void Delete(TId id);
        void Delete(TEntity entity);
        Task DeleteAsync(TId id, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
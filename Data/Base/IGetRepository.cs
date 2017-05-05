﻿using System.Threading;
using System.Threading.Tasks;

namespace JessicasAquariumMonitor.Data.Base
{
    public interface IGetRepository<TEntity, in TId>
    {
        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
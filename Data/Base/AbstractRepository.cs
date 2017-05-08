using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JessicasAquariumMonitor.Data.Base
{
    internal abstract class AbstractRepository<TContext, TKey, TEntity> : IBulkGetRepository<TEntity, TKey>,
        IBulkAddRepository<TEntity, TKey>, IBulkDeleteRepository<TEntity, TKey>
        where TContext : DbContext where TEntity : class
    {
        private readonly TContext _providedContext;

        protected AbstractRepository(TContext context)
        {
            _providedContext = context;
        }

        protected TContext Context => CurrentUnitOfWork?.Context ?? _providedContext;
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

        private static UnitOfWork<TContext> CurrentUnitOfWork => UnitOfWork<TContext>.CurrentUnitOfWork;

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);

            SaveChanges();
        }

        public virtual void AddAll(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);

            SaveChanges();
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteThenSaveAsync(() => DbSet.Add(entity), cancellationToken);

        public virtual Task AddAllAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteThenSaveAsync(() => DbSet.AddRange(entities), cancellationToken);

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);

            SaveChanges();
        }

        public virtual void DeleteAll(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);

            SaveChanges();
        }

        public virtual void Delete(TKey key)
        {
            Delete(Get(key));

            SaveChanges();
        }

        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteThenSaveAsync(() => DbSet.Remove(entity), cancellationToken);

        public virtual Task DeleteAllAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteThenSaveAsync(() => DbSet.RemoveRange(entities), cancellationToken);

        public virtual Task DeleteAsync(TKey key, CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteThenSaveAsync(() => DbSet.Remove(GetAsync(key, cancellationToken).Result), cancellationToken);

        public abstract TEntity Get(TKey key);

        public virtual Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => Get(key), cancellationToken);

        public virtual IEnumerable<TEntity> GetAll() => DbSet;

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbSet
                .ToArrayAsync(cancellationToken)
                .ContinueWith(retrieveTask =>
                {
                    var results = retrieveTask.Result;

                    return results.AsEnumerable();
                }, cancellationToken);
        }

        private Task ExecuteThenSaveAsync(Action action,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task
                .Run(action, cancellationToken)
                .ContinueWith(task =>
                {
                    task.Wait(cancellationToken);

                    return SaveChangesAsync(cancellationToken);
                }, cancellationToken);
        }

        public void SaveChanges()
        {
            if (CurrentUnitOfWork == null)
            {
                Context.SaveChanges();
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CurrentUnitOfWork == null)
            {
                return Context.SaveChangesAsync(cancellationToken);
            }

            return Task.FromResult(true);
        }
    }
}
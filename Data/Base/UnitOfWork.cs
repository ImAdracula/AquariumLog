using System;
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace JessicasAquariumMonitor.Data.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsReadonlyTransaction { get; }
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Abort();
        Task AbortAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    internal sealed class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        [ThreadStatic] public static UnitOfWork<TContext> CurrentUnitOfWork;
        private readonly IsolationLevel _isolationLevel;
        private readonly UnitOfWork<TContext> _previousUnitOfWork;

        public UnitOfWork(TContext context, IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
            Context = context;

            DisposeCurrentTransaction();
            BeginNewTransaction();

            _previousUnitOfWork = CurrentUnitOfWork;

            IsReadonlyTransaction = _isolationLevel != IsolationLevel.ReadCommitted;
            CurrentUnitOfWork = this;
        }

        public TContext Context { get; }
        private DbContextTransaction CurrentTransaction => Context.Database.CurrentTransaction;
        public bool IsReadonlyTransaction { get; }

        public void Commit()
        {
            Context.SaveChanges();

            CommitAndBeginNewTransaction();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
            => Context
                .SaveChangesAsync(cancellationToken)
                .ContinueWith(task =>
                {
                    task.Wait(cancellationToken);

                    CommitAndBeginNewTransaction();
                }, cancellationToken);

        public void Abort() => CurrentTransaction.Rollback();

        public Task AbortAsync(CancellationToken cancellationToken = default(CancellationToken))
            => Task.Run(() => CurrentTransaction.Rollback(), cancellationToken);

        public void Dispose()
        {
            DisposeCurrentTransaction();

            CurrentUnitOfWork = _previousUnitOfWork;
        }

        private void CommitAndBeginNewTransaction()
        {
            CurrentTransaction?.Commit();
            BeginNewTransaction();
        }

        private void BeginNewTransaction() => Context.Database.BeginTransaction(_isolationLevel);
        private void DisposeCurrentTransaction() => CurrentTransaction?.Dispose();
    }
}
using System.Data;
using DryIoc;

namespace Data.Base
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateAquariumReadOnlyUnitOfWork();
        IUnitOfWork CreateAquariumReadWriteUnitOfWork();
    }

    internal sealed class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IResolver _resolver;

        public UnitOfWorkFactory(IResolver resolver)
        {
            _resolver = resolver;
        }

        public IUnitOfWork CreateAquariumReadOnlyUnitOfWork()
            => CreateAquariumUnitOfWork(IsolationLevel.ReadUncommitted);

        public IUnitOfWork CreateAquariumReadWriteUnitOfWork()
            => CreateAquariumUnitOfWork(IsolationLevel.ReadCommitted);

        private IUnitOfWork CreateAquariumUnitOfWork(IsolationLevel isolationLevel)
        {
            var context = _resolver.Resolve<AquariumContext>();

            return new UnitOfWork<AquariumContext>(context, isolationLevel);
        }
    }
}
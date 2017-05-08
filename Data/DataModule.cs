using DryIoc;
using JessicasAquariumMonitor.Data.Base;
using JessicasAquariumMonitor.Data.Repositories;
using JessicasAquariumMonitor.Helpers.DependencyInjection;

namespace JessicasAquariumMonitor.Data
{
    public sealed class DataModule : IModule
    {
        private DataModule()
        {
        }

        public static DataModule Instance { get; } = new DataModule();

        public void Load(IRegistrator registrator)
        {
            registrator.RegisterForAllImplementedInterfaces(typeof(LogEntryRepository), Reuse.InCurrentScope);
            registrator.RegisterForAllImplementedInterfaces(typeof(ChemicalTypeRepository), Reuse.InCurrentScope);
            registrator.RegisterForAllImplementedInterfaces(typeof(FilterTypeRepository), Reuse.InCurrentScope);

            registrator.RegisterDelegate(typeof(IUnitOfWorkFactory), resolver => new UnitOfWorkFactory(resolver),
                Reuse.Singleton);

            registrator.RegisterDelegate(resolver => new AquariumContext("AquariumDatabase"), Reuse.Transient,
                Setup.With(trackDisposableTransient: true));
        }
    }
}
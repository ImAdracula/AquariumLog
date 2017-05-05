using DryIoc;
using JessicasAquariumMonitor.Helpers.Caching;
using JessicasAquariumMonitor.Helpers.DependencyInjection;

namespace JessicasAquariumMonitor.Helpers
{
    public sealed class HelpersModule : IModule
    {
        private HelpersModule()
        {
        }

        public static HelpersModule Instance { get; } = new HelpersModule();

        public void Load(IRegistrator registrator)
        {
            registrator.RegisterIfNotAlready<ICacheProvider, CacheProvider>(Reuse.Singleton);
        }
    }
}
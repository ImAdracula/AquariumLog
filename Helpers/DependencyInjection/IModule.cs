using DryIoc;

namespace JessicasAquariumMonitor.Helpers.DependencyInjection
{
    public interface IModule
    {
        void Load(IRegistrator registrator);
    }
}
using System;
using System.Linq;
using DryIoc;
using JessicasAquariumMonitor.Data;
using JessicasAquariumMonitor.Helpers;
using JessicasAquariumMonitor.Helpers.DependencyInjection;
using JessicasAquariumMonitor.Helpers.General;

namespace JessicasAquariumMonitor.Web
{
    public sealed class WebModule : IModule
    {
        private readonly Type[] _conversionTypes;

        private WebModule()
        {
            _conversionTypes =
                this.ThisAssemblyConcreteTypesImplementingTheseInterfaces(typeof(IConverter<,>)).ToArray();
        }

        public static WebModule Instance { get; } = new WebModule();

        public void Load(IRegistrator registrator)
        {
            registrator.RegisterAll(DataModule.Instance);
            registrator.RegisterAll(HelpersModule.Instance);

            registrator
                .RegisterForAllImplementedInterfaces(_conversionTypes, Reuse.Singleton);
        }
    }
}
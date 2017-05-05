﻿using Data.Base;
using Data.Repositories;
using DryIoc;
using JessicasAquariumMonitor.Helpers.DependencyInjection;

namespace Data
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
            registrator.RegisterDelegate(resolver => new UnitOfWorkFactory(resolver), Reuse.Singleton);
            registrator.RegisterDelegate(resolver => new AquariumContext("AquariumDatabase"));
        }
    }
}
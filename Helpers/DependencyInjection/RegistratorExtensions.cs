using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIoc;

namespace JessicasAquariumMonitor.Helpers.DependencyInjection
{
    public static class RegistratorExtensions
    {
        public static void RegisterAll(this IRegistrator registrator, IModule module) => module.Load(registrator);

        public static IEnumerable<Type> ThisAssemblyTypes<TModule>(this TModule module) where TModule : IModule
            => module.ThisAssembly().GetTypes();

        public static Assembly ThisAssembly<TModule>(this TModule module) where TModule : IModule
            => typeof(TModule).Assembly;

        public static IEnumerable<Type> ThisAssemblyConcreteTypes<TModule>(this TModule module) where TModule : IModule
            => module.ThisAssemblyTypes().Where(type => type.IsClass && !type.IsAbstract);

        public static void RegisterForAllImplementedInterfaces(this IRegistrator registrator, IEnumerable<Type> types,
            IReuse reuse = null)
        {
            foreach (var type in types)
            {
                registrator.RegisterForAllImplementedInterfaces(type, reuse);
            }
        }

        public static void RegisterForAllImplementedInterfaces(this IRegistrator registrator, Type type,
            IReuse reuse = null)
        {
            var implementedTypes = type.GetImplementedTypes();

            foreach (var implementedType in implementedTypes)
            {
                registrator.RegisterIfNotAlready(implementedType, type, reuse);
            }
        }

        public static void RegisterForAllImplementedInterfaces<T>(this IRegistrator registrator, IReuse reuse = null)
            => registrator.RegisterForAllImplementedInterfaces(typeof(T));

        public static IEnumerable<Type> ThisAssemblyConcreteTypesImplementingTheseInterfaces<TModule>(
            this TModule module, params Type[] interfaceTypes) where TModule : IModule
        {
            var concreteTypes = module.ThisAssemblyConcreteTypes();

            return concreteTypes
                .Where(
                    type =>
                        type.GetInterfaces()
                            .Any(
                                implementedInterface =>
                                    interfaceTypes.Any(interfaceType => implementedInterface == interfaceType)));
        }

        public static void RegisterIfNotAlready(this IRegistrator registrator, Type interfaceType,
            Type implementationType, IReuse reuse = null)
        {
            if (!registrator.IsRegistered(interfaceType))
            {
                registrator.Register(interfaceType, implementationType, reuse);
            }
        }

        public static void RegisterIfNotAlready<TInterface, TImplementation>(this IRegistrator registrator,
            IReuse reuse = null) where TImplementation : TInterface => registrator.RegisterIfNotAlready(typeof(TInterface), typeof(TImplementation), reuse);
    }
}
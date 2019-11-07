using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MWJZ.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddDependency(this IServiceCollection serviceCollection)
        {
            var serviceRegistry = new ServiceRegistry(new DependencyTypeService());
            foreach (var serviceDescriptor in serviceRegistry.GetServiceDescriptors())
                serviceCollection.Add(serviceDescriptor);
        }

        public static IServiceCollection IncludeAssembly(this IServiceCollection serviceCollection, string assemblyName)
        {
            var name = new AssemblyName(assemblyName ?? throw new ArgumentNullException(nameof(assemblyName)));
            var assembly = Assembly.Load(name);
            return serviceCollection.IncludeAssembly(assembly);
        }

        public static IServiceCollection IncludeAssembly(this IServiceCollection serviceCollection, Assembly assembly)
        {
            DependencyAssemblyCache.Current.AddAssembly(assembly);
            return serviceCollection;
        }
    }
}
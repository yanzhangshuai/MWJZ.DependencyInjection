using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MWJZ.DependencyInjection.Infrastructure;

namespace MWJZ.DependencyInjection
{
    public class ServiceRegistry
    {
        private readonly IDependencyTypeService _dependencyTypeService;

        public ServiceRegistry([NotNull] IDependencyTypeService dependencyTypeService)
        {
            _dependencyTypeService = dependencyTypeService;
        }

        public IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            var serviceDescriptors = new List<ServiceDescriptor>();
            var assembles = DependencyAssemblyCache.Current.GetAllAssembly();
            foreach (var dependencyType in _dependencyTypeService.GetDependencyTypes(assembles))
            {
                if (dependencyType.ServiceTypes.Any())
                    serviceDescriptors.AddRange(dependencyType.ServiceTypes
                        .Select(serviceType =>
                            new ServiceDescriptor(serviceType, dependencyType.ImplementationType,
                                dependencyType.Lifetime)));
                else
                    serviceDescriptors.Add(new ServiceDescriptor(dependencyType.ImplementationType,
                        dependencyType.ImplementationType, dependencyType.Lifetime));
            }

            return serviceDescriptors;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MWJZ.DependencyInjection.Dependency;
using MWJZ.DependencyInjection.Infrastructure;

namespace MWJZ.DependencyInjection
{
    internal class DependencyTypeService : IDependencyTypeService
    {
        public IList<DependencyType> GetDependencyTypes(params Assembly[] assemblies)
        {
            var allDependencyType = new List<DependencyType>();
            foreach (var type in assemblies.SelectMany(assembly => assembly.GetTypes()))
            {
                if (!type.IsClass || type.IsAbstract) continue;
                if (typeof(ISingletonDependency).IsAssignableFrom(type))
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Singleton, type,
                        GetServiceType<ISingletonDependency>(type, assemblies)));
                else if (typeof(IScopedDependency).IsAssignableFrom(type))
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Transient, type,
                        GetServiceType<IScopedDependency>(type, assemblies)));
                else if (typeof(ITransientDependency).IsAssignableFrom(type))
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Scoped, type,
                        GetServiceType<ITransientDependency>(type, assemblies)));

                var singletonAttributeServices =
                    GetServiceType(type.GetCustomAttributes<SingletonDependencyAttribute>().Select(y => y.ServiceType),
                        assemblies);
                var scopedAttributeServices =
                    GetServiceType(type.GetCustomAttributes<ScopedDependencyAttribute>().Select(y => y.ServiceType),
                        assemblies);
                var transientAttributeServices =
                    GetServiceType(type.GetCustomAttributes<TransientDependencyAttribute>().Select(y => y.ServiceType),
                        assemblies);

                if (singletonAttributeServices.Any())
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Singleton, type,
                        singletonAttributeServices));
                if (scopedAttributeServices.Any())
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Scoped, type, singletonAttributeServices));
                if (transientAttributeServices.Any())
                    allDependencyType.Add(new DependencyType(ServiceLifetime.Transient, type,
                        singletonAttributeServices));
            }

            return allDependencyType;
        }

        private Type[] GetServiceType(IEnumerable<Type> serviceType, params Assembly[] includeAssemblies)
        {
            if (includeAssemblies.Any() != true)
                return serviceType.ToArray();
            var allTypes = includeAssemblies.SelectMany(assembly => assembly.GetTypes());
            return serviceType.Intersect(allTypes).Distinct().ToArray();
        }

        private Type[] GetServiceType<T>(Type implementType, params Assembly[] includeAssemblies)
        {
            var interfaces = implementType.GetInterfaces()
                .Where(type =>
                    typeof(T) != type);
            if (includeAssemblies.Any() != true)
                return interfaces.ToArray();
            var allTypes = includeAssemblies.SelectMany(assembly => assembly.GetTypes());
            return interfaces.Intersect(allTypes).Distinct().ToArray();
        }
    }
}
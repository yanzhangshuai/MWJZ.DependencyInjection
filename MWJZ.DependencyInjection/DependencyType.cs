using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MWJZ.DependencyInjection.Dependency;

namespace MWJZ.DependencyInjection
{
    public class DependencyType
    {
        public DependencyType(ServiceLifetime lifetime, Type implementationType, Type[] serviceTypes = null)
        {
            Lifetime = lifetime;
            ImplementationType = implementationType;
            ServiceTypes = serviceTypes;
        }

        public ServiceLifetime Lifetime { get; set; }

        public Type ImplementationType { get; set; }

        public Type[] ServiceTypes { get; set; }

        public Type[] GetInjectionInterfaces()
        {
            //    这处理的不好
            return ImplementationType.GetInterfaces()
                .Where(type =>
                    typeof(IScopedDependency) != type && typeof(ISingletonDependency) != type &&
                    typeof(ITransientDependency) != type).ToArray();
        }
    }
}
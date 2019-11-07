using System;

namespace MWJZ.DependencyInjection.Dependency
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedDependencyAttribute : Attribute
    {
        public ScopedDependencyAttribute(Type serviceType = null)
        {
            ServiceType = serviceType;
        }

        public Type ServiceType { get; set; }

        public int Order { get; set; }
    }
}
using System.Collections.Generic;
using System.Reflection;

namespace MWJZ.DependencyInjection.Infrastructure
{
    public interface IDependencyTypeService
    {
        IList<DependencyType> GetDependencyTypes(params Assembly[] assembly);
    }
}
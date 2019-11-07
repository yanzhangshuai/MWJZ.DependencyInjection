using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MWJZ.DependencyInjection.Test
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            #region test

//                        var dlls = DependencyContext.Default.CompileLibraries
//                .SelectMany(x => x.ResolveReferencePaths())
//                .Distinct()
//                .Where(x => x.Contains(Directory.GetCurrentDirectory()))
//                .ToList();
//            foreach (var ddk in dlls)
//            {
//                Console.WriteLine(ddk);
//            }

            // Console.WriteLine( Assembly.GetEntryAssembly().GetName());
            // ConventionalRegistrar.GetAllAssembly();
//            Console.WriteLine(typeof(B).GetInterfaces().Count());

            #endregion
            await new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.IncludeAssembly("MWJZ.DependencyInjection.Test")
                        .AddDependency();
                    services.AddHostedService<DependencyInjectionHostedService>();
                })
                .RunConsoleAsync();
        }
    }
}
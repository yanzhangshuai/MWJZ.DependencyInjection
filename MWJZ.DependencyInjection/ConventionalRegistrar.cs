using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace MWJZ.DependencyInjection
{
    public class ConventionalRegistrar
    {
        public static CompilationLibrary[] GetAllAssembly()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
//            var dlls = new List<string>();
//            foreach (var compilationLibrary in DependencyContext.Default.CompileLibraries)
//            {
//                foreach (var resolveReferencePath in compilationLibrary.ResolveReferencePaths())
//                {
//                    Console.WriteLine($"\t\tReference path: {resolveReferencePath}");
//                    dlls.Add(resolveReferencePath);
//                }
//            }
//             dlls = dlls.Distinct().ToList();
            var deps = DependencyContext.Default;
            foreach (var compilationLibrary in deps.CompileLibraries)
            {
                Console.WriteLine("==================");
                foreach (var a in compilationLibrary.ResolveReferencePaths(new PackageCompilationAssemblyResolver()))
                    Console.WriteLine("123" + a);

//                Console.WriteLine($"\tPackage {compilationLibrary.PackageName} {compilationLibrary.Version}");
//                // ResolveReferencePaths returns full paths to compilation assemblies
//                foreach (var resolveReferencePath in compilationLibrary.ResolveReferencePaths())
//                {
//                    Console.WriteLine($"\t\tReference path: {resolveReferencePath}");
//                }
            }
//            var dlls = DependencyContext.Default.CompileLibraries
//
//                .Distinct()
//                .Where(x => x.Contains(Directory.GetCurrentDirectory()))
//                .ToList();
//            foreach (var name in dlls)
//            {
//                Console.WriteLine(name);
//            }

            return
                DependencyContext.Default.CompileLibraries.Where(y =>
                    !y.Name.StartsWith("System.") && !y.Name.StartsWith("Microsoft.")).ToArray();
        }
    }
}
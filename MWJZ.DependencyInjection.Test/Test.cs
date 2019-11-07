using System;
using MWJZ.DependencyInjection.Dependency;

namespace MWJZ.DependencyInjection.Test
{
    [TransientDependency(typeof(D))]
    class D
    {
        public void Show()
        {
            Console.WriteLine("这是D");
        }
    }

    internal abstract class C : IC
    {
        public abstract void Show();
    }

    internal class B : C, ITransientDependency
    {
        public override void Show()
        {
            Console.WriteLine("这是IC-B");
        }
    }

    internal class B1 : IC, ITransientDependency
    {
        public void Show()
        {
            Console.WriteLine("这是IC-B2");
        }
    }

    internal interface IC
    {
        void Show();
    }
    
    public interface IA
    {
        void Show();
    }

    public class A : ITransientDependency
    {
        public void Show()
        {
            Console.WriteLine("Show");
        }
    }
}
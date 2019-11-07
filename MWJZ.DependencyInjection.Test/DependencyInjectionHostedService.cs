using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MWJZ.DependencyInjection.Test
{
    public class DependencyInjectionHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DependencyInjectionHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await new ValueTask();
            var scope = _serviceScopeFactory.CreateScope();
            var a = scope.ServiceProvider.GetService<A>();
            a.Show();
            var d = scope.ServiceProvider.GetService<D>();
            d.Show();
            var c = scope.ServiceProvider.GetServices<IC>();
            foreach (var c1 in c) c1.Show();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
        }
    }
}
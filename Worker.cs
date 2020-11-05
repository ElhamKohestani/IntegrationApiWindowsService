using System.Threading;
using System.Threading.Tasks;
using IntegrationApiSynchroniser.Infrastructure.Services;
using Microsoft.Extensions.Hosting;

namespace IntegrationApiSynchroniser
{
    public class Worker : BackgroundService
    {
        private ISyncService _synService;

        public Worker(ISyncService syncService)
        {
            _synService = syncService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            // Task retry failed records
            await _synService.Sync(stoppingToken);

        }
    }
}

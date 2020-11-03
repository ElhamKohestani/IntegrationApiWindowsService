using System.Threading;
using System.Threading.Tasks;
using IntegrationApiSynchroniser.Infrastructure.Services;
using Microsoft.Extensions.Hosting;

namespace IntegrationApiSynchroniser
{
    public class Worker : BackgroundService
    {
        private ISyncService _synService;
        private IUpdateTokenService _tokenUpdateService;

        public Worker(ISyncService syncService, IUpdateTokenService tokenUpdateService)
        {
            _synService = syncService;
            _tokenUpdateService = tokenUpdateService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            // Task retry failed records
            await _synService.Sync(stoppingToken);

            //Task update token 
            await _tokenUpdateService.UpdateToken(stoppingToken);

        }
    }
}

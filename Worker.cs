using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using IntegrationApiSynchroniser.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IntegrationApiSynchroniser
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _servicProvider;
        private readonly ISyncService _synService;
        

        public Worker(ILogger<Worker> logger,IServiceProvider serviceProvider, ISyncService syncService)
        {
            _logger = logger;
            _servicProvider = serviceProvider;
            _synService = syncService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _servicProvider.CreateScope())
            {
                ISyncService syncService = _servicProvider.GetRequiredService<ISyncService>();
                await syncService.Sync(stoppingToken);
            }
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}

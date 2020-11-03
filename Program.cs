using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiAccountService;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationApiSynchroniser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Scoped services
                    
                    
                    services.AddDbContext<WorkerContext>();

                    // Singleton services
                    services.AddSingleton<IApiClientServices, ApiClientServices>();
                    services.AddSingleton<IApiAccountService, ApiAccountService>();
                    services.AddSingleton<ISyncService, SyncService>();
                    services.AddSingleton<IUpdateTokenService, UpdateTokenService>();

                    // Hosted background services
                    services.AddHostedService<Worker>();
                });
    }
}

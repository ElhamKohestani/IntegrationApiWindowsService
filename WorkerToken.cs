using System.Threading;
using System.Threading.Tasks;
using IntegrationApiSynchroniser.Infrastructure.Services;
using Microsoft.Extensions.Hosting;


namespace IntegrationApiSynchroniser
{
    public class WorkerToken : BackgroundService
    {
      
        private IUpdateTokenService _tokenUpdateService;

        public WorkerToken(IUpdateTokenService tokenUpdateService)
        {
            _tokenUpdateService = tokenUpdateService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Task update token 
            await _tokenUpdateService.UpdateToken(stoppingToken);
        }
    }
   
}

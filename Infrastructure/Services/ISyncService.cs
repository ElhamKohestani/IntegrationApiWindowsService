using System.Threading;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    public interface ISyncService
    {
        public Task Sync(CancellationToken stoppingToken);
    }
}

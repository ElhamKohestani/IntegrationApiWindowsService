using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    public interface IApiClientServices 
    {
        Task<HttpResponseMessage> GetAsync(string url, string tokan, string parameter);
        Task PostAsync(string url, string tokan, string parameter);
    }
}

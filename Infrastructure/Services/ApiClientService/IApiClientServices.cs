using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService
{
    public interface IApiClientServices<T> where T : class
    {
        Task<T> GetAsync(string url, string tokan, string Id);

        Task<IEnumerable<T>> GetAllAsync(string url, string tokan);

        Task<T> PostAsync(string url, string tokan, T objCreate);

        Task<HttpResponseMessage> PostBasicAsync(string url, string tokan, T objCreate);

        Task<T> PutAsync(string url, string tokan, T objCreate);

        Task<bool> DeleteAsync(string url, string tokan, string Id);
    }
}

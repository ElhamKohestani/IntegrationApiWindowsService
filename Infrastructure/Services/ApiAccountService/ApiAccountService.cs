using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IntegrationApiSynchroniser.Infrastructure.Helpers;
using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService;

namespace IntegrationApiSynchroniser.Infrastructure.Services.ApiAccountService
{
    public class ApiAccountService : ApiClientServices, IApiAccountService
    {
        public UserLoginDto Authenticate(UserLoginDto model)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiClientHelper.baseUrlArd)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(contentType);

            try
            {
                message = client.PostAsJsonAsync(ApiClientHelper.baseUrlAccountAuthenticateArd, model).Result;
                if (message.IsSuccessStatusCode)
                {
                    return message.Content.ReadAsAsync<UserLoginDto>().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new UserLoginDto();
        }
    }
}
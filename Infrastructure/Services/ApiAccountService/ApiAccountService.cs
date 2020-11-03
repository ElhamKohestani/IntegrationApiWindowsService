using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IntegrationApiSynchroniser.Infrastructure.Helpers;
using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService;
using Microsoft.Extensions.Configuration;

namespace IntegrationApiSynchroniser.Infrastructure.Services.ApiAccountService
{
    public class ApiAccountService : ApiClientServices, IApiAccountService
    {
        private IConfiguration _configuration;

        public ApiAccountService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public UserLoginDto Authenticate(UserLoginDto model)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_configuration.GetValue<string>("ARD_BASE_URL"))
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(contentType);

            try
            {
                message = client.PostAsJsonAsync(_configuration.GetValue<string>("ARD_AUTHENTICATION_URI"), model).Result;
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
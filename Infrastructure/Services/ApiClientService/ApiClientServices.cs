using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IntegrationApiSynchroniser.Infrastructure.Exceptions;
using IntegrationApiSynchroniser.Infrastructure.Helpers;

namespace IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService
{
    public class ApiClientServices : IApiClientServices
    {
        protected HttpResponseMessage message;
        protected HttpClient webApiClient;
        public ApiClientServices()
        {
            message = new HttpResponseMessage();
            webApiClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string Url, string tokan, string parameter)
        {
            using (var client = this.HeadersForAccessTokenGenerate(tokan, Url))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(parameter);
                    parameter = System.Convert.ToBase64String(plainTextBytes);

                    request.Headers.Add("acbr_token_auth", tokan);
                    request.Headers.Add("queryParams", parameter);
                    using (var response = await client
                           .SendAsync(request)
                           .ConfigureAwait(false))
                    {

                        return response.EnsureSuccessStatusCode();
                    }
                }
            }

        }

        public async Task PostAsync(string Url, string tokan, string parameter)
        {
            using (var client = this.HeadersForAccessTokenGenerate(tokan, Url))
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                request.Headers.Add("acbr_token_auth", tokan);
                request.Headers.Add("queryParams", parameter);
                using (var response = await client
                       .SendAsync(request)
                       .ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        protected HttpClient HeadersForAccessTokenGenerate(string tokan, string url)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = new TimeSpan(0, 0, 120);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokan);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return client;
        }
    }
}
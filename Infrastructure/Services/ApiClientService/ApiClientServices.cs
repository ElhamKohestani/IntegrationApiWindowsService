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
    public class ApiClientServices<T> : IApiClientServices<T> where T : class
    {
        protected HttpResponseMessage message;
        protected HttpClient webApiClient;
        public ApiClientServices()
        {
            message = new HttpResponseMessage();
            webApiClient = new HttpClient();
        }

        public async Task<T> GetAsync(string url, string tokan, string Id)
        {
            webApiClient = this.HeadersForAccessTokenGenerate(tokan);
            string endPointUrl = string.Format("{0}/{1}", url, Id);
            message = await webApiClient.GetAsync(endPointUrl);

            if (message.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            return message.Content.ReadAsAsync<T>().Result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url, string tokan)
        {
            webApiClient = this.HeadersForAccessTokenGenerate(tokan);
            message = await webApiClient.GetAsync(url);

            if (message.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var retContent = message.Content.ReadAsStringAsync().Result;
            List<T> ret = JsonConvert.DeserializeObject<List<T>>(retContent);

            return ret;
        }

        public async Task<T> PostAsync(string url, string tokan, T objCreate)
        {
            webApiClient = this.HeadersForAccessTokenGenerate(tokan);
            try
            {
                message = await webApiClient.PostAsJsonAsync<T>(url, objCreate);

                if (message.IsSuccessStatusCode)
                {
                    T ret = message.Content.ReadAsAsync<T>().Result;

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        public async Task<HttpResponseMessage> PostBasicAsync(string Url, string tokan, T objCreate)
        {
            using (var client = this.HeadersForAccessTokenGenerate(tokan))
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var json = JsonConvert.SerializeObject(objCreate);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

    
                    using (var response = await client
                        .SendAsync(request)
                        .ConfigureAwait(false))
                    {

                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                            return response;
                        else
                            throw new ResponseException() { StatusCode = response.StatusCode, ResponseMessage = "Request to ARD Failed" };
                    }
                }
            }
        }

        public async Task<T> PutAsync(string url, string tokan, T objCreate)
        {
            webApiClient = this.HeadersForAccessTokenGenerate(tokan);

            try
            {
                message = await webApiClient.PutAsJsonAsync<T>(url, objCreate);

                if (message.IsSuccessStatusCode)
                {
                    T ret = message.Content.ReadAsAsync<T>().Result;

                    return ret;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(string url, string tokan, string Id)
        {
            webApiClient = this.HeadersForAccessTokenGenerate(tokan);
            string endPointUrl = string.Format("{0}/{1}", url, Id);

            try
            {
                message = await webApiClient.DeleteAsync(endPointUrl);

                if (message.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                return message.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected HttpClient HeadersForAccessTokenGenerate(string tokan)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(ApiClientHelper.baseUrlArd);
                client.Timeout = new TimeSpan(0, 0, 120);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokan);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return client;
        }
    }
}
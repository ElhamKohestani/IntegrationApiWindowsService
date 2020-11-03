using System;
using System.Net;

namespace IntegrationApiSynchroniser.Infrastructure.Exceptions
{
    public class ResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}

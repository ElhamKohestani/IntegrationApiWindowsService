using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationApiSynchroniser.Infrastructure.Helpers
{
    public static class ApiClientHelper
    {
        // ARD
        public readonly static string baseUrlArd = "https://sigtas-api.conveyor.cloud";
        public readonly static string baseUrlAccountArd = string.Format("{0}/{1}", baseUrlArd, "api/v1/Account");
        public readonly static string baseUrlAccountAuthenticateArd = string.Format("{0}/{1}", baseUrlAccountArd, "Authenticate");
    }
}

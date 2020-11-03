using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationApiSynchroniser.Infrastructure.Helpers
{
    public static class ApiClientHelper
    {
        public readonly static string baseUrlArd = "https://sigtas-api.conveyor.cloud";
        public readonly static string baseUrlAccountArd = string.Format("{0}/{1}", baseUrlArd, "api/v1/Account");
        public readonly static string baseUrlAccountAuthenticateArd = string.Format("{0}/{1}", baseUrlAccountArd, "Authenticate");

        public readonly static string baseUrlTcc = string.Format("{0}/{1}", baseUrlArd, "api/v2/LetterApp");

        //enterprise data post
        public readonly static string baseUrlEnterpise = string.Format("{0}/{1}", baseUrlArd, "api/v1/EnterpriseTP");

        public readonly static string baseUrlAddress = string.Format("{0}/{1}", baseUrlArd, "api/v1/Addresses");
        public readonly static string baseUrlEnterpriseSectorActivities = string.Format("{0}/{1}", baseUrlArd, "api/v1/EnterpriseTPSectorActivity");
        public readonly static string baseUrlEnterpriseType = string.Format("{0}/{1}", baseUrlArd, "api/v1/EnterpriseTPType");
        public readonly static string baseUrlIndividual = string.Format("{0}/{1}", baseUrlArd, "api/v1/IndividualTP");
        public readonly static string baseUrlEnterpriseBusinessActivies = string.Format("{0}/{1}", baseUrlArd, "api/v1/EnterpriseTPBusinessActivities");
        public readonly static string baseUrlEnterpriseActivies = string.Format("{0}/{1}", baseUrlArd, "api/v1/EnterpriseTPActivity");
    }
}

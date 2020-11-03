using Microsoft.Extensions.Configuration;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace IntegrationApiSynchroniser.Common
{
    public class Settings
    {
        private IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ConnectionString 
        { 
            get
            {
                return _configuration.GetValue<string>("APIConnectionString");
            }
            private set { }
        }
    }
}

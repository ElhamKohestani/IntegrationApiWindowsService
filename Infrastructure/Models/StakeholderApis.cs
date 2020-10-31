using System;
using System.Collections.Generic;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class StakeholderApis
    {
        public int Id { get; set; }
        public int StakeholderId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }
}

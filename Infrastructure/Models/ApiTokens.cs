using System;
using System.Collections.Generic;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class ApiTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? StakeholderId { get; set; }
    }
}

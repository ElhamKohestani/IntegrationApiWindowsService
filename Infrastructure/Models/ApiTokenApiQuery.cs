using System;
using System.Collections.Generic;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class ApiTokenApiQuery
    {
        public int Id { get; set; }
        public int TokenId { get; set; }
        public int ApiId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class Api
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Parameter { get; set; }
    }
}

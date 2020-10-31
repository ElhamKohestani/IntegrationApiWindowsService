using System;
using System.Collections.Generic;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class TblApiSync
    {
        public int ApiSynId { get; set; }
        public int ApiId { get; set; }
        public string Parameter { get; set; }
        public string EndPoint { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool SyncStatus { get; set; }
        public int? TryCount { get; set; }
    }
}

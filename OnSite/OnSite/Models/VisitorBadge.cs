using System;
using System.Collections.Generic;

namespace OnSite.Models
{
    public partial class VisitorBadge
    {
        public VisitorBadge()
        {
            Visit = new HashSet<Visit>();
        }

        public int BadgeId { get; set; }
        public string BadgeType { get; set; }
        public int SiteId { get; set; }

        public Site Site { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

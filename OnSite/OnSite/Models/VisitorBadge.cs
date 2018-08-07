using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class VisitorBadge
    {
        public VisitorBadge()
        {
            Visit = new HashSet<Visit>();
        }

        public int BadgeId { get; set; }
        [Display(Name="Badge Type")]
        public string BadgeType { get; set; }
        [Display(Name="Site")]
        public int SiteId { get; set; }

        public Site Site { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class Area
    {
        public Area()
        {
            StaffAreaAccess = new HashSet<StaffAreaAccess>();
            Visit = new HashSet<Visit>();
        }

        public int AreaId { get; set; }
        public string Floor { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        [Display(Name="Site")]
        public int SiteId { get; set; }

        public Site Site { get; set; }
        public ICollection<StaffAreaAccess> StaffAreaAccess { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

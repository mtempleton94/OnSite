using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class Site
    {
        public Site()
        {
            Area = new HashSet<Area>();
            Visit = new HashSet<Visit>();
            VisitorBadge = new HashSet<VisitorBadge>();
        }

        public int SiteId { get; set; }
        [Display(Name="Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name="Post Code")]
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Organisation")]
        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }
        public ICollection<Area> Area { get; set; }
        public ICollection<Visit> Visit { get; set; }
        public ICollection<VisitorBadge> VisitorBadge { get; set; }
    }
}

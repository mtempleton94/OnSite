using System;
using System.Collections.Generic;

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
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }
        public ICollection<Area> Area { get; set; }
        public ICollection<Visit> Visit { get; set; }
        public ICollection<VisitorBadge> VisitorBadge { get; set; }
    }
}

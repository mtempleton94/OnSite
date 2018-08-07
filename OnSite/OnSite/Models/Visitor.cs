using System;
using System.Collections.Generic;

namespace OnSite.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            Visit = new HashSet<Visit>();
        }

        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

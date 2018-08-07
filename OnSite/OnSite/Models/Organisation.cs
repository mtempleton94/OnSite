using System;
using System.Collections.Generic;

namespace OnSite.Models
{
    public partial class Organisation
    {
        public Organisation()
        {
            Site = new HashSet<Site>();
            Visitor = new HashSet<Visitor>();
        }

        public int OrganisationId { get; set; }
        public string Name { get; set; }

        public ICollection<Site> Site { get; set; }
        public ICollection<Visitor> Visitor { get; set; }
    }
}

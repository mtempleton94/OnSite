using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            Visit = new HashSet<Visit>();
        }

        public int VisitorId { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name="From Organisation")]
        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

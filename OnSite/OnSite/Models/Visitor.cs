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
        [Required]
        [RegularExpression(@"^[A-Za-z ,.'-]+$", ErrorMessage = "Make it Valid.")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z ,.'-]+$", ErrorMessage = "Make it Valid.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name="From Organisation")]
        [Range(1, int.MaxValue, ErrorMessage = "Select an Organisation.")]
        public int OrganisationId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9 -]+$", ErrorMessage = "Make it Valid.")]
        [Display(Name = "Passport or Licence Number")]
        public string IdentificationNumber { get; set; }

        public Organisation Organisation { get; set; }
        public ICollection<Visit> Visit { get; set; }
    }
}

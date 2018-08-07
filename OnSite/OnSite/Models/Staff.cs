using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class Staff
    {
        public Staff()
        {
            StaffAreaAccess = new HashSet<StaffAreaAccess>();
            VisitSignedInBy = new HashSet<Visit>();
            VisitStaffEscort = new HashSet<Visit>();
            VisitUnescortedApprovedBy = new HashSet<Visit>();
        }

        public int StaffId { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<StaffAreaAccess> StaffAreaAccess { get; set; }
        public ICollection<Visit> VisitSignedInBy { get; set; }
        public ICollection<Visit> VisitStaffEscort { get; set; }
        public ICollection<Visit> VisitUnescortedApprovedBy { get; set; }
    }
}

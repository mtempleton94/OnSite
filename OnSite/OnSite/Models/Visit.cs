using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class Visit
    {
        public int VisitId { get; set; }
        public string Description { get; set; }
        [Display(Name="Sign In Time")]
        public DateTime SignInTime { get; set; }
        [Display(Name = "Sign Out Time")]
        public DateTime? SignOutTime { get; set; }
        [Display(Name = "Vehicle Registration")]
        public string VehicleId { get; set; }
        [Display(Name = "Visitor")]
        public int VisitorId { get; set; }
        [Display(Name = "Assigned Badge")]
        public int BadgeId { get; set; }
        [Display(Name = "Signed In By")]
        public int SignedInById { get; set; }
        [Display(Name = "Escort")]
        public int StaffEscortId { get; set; }
        [Display(Name = "Unescorted Approved By")]
        public int UnescortedApprovedById { get; set; }
        [Display(Name="Site")]
        public int SiteId { get; set; }
        [Display(Name="Areas Approved to Access")]
        public int? AreaId { get; set; }
        [Display(Name ="Visit Approved (true/false)")]
        public bool ApprovalStatus { get; set; }

        public Area Area { get; set; }
        public VisitorBadge Badge { get; set; }
        public Staff SignedInBy { get; set; }
        public Site Site { get; set; }
        public Staff StaffEscort { get; set; }
        public Staff UnescortedApprovedBy { get; set; }
        public Visitor Visitor { get; set; }
    }
}

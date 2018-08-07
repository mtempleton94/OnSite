using System;
using System.Collections.Generic;

namespace OnSite.Models
{
    public partial class Visit
    {
        public int VisitId { get; set; }
        public string Description { get; set; }
        public DateTime SignInTime { get; set; }
        public DateTime? SignOutTime { get; set; }
        public string VehicleId { get; set; }
        public int VisitorId { get; set; }
        public int BadgeId { get; set; }
        public int SignedInById { get; set; }
        public int StaffEscortId { get; set; }
        public int UnescortedApprovedById { get; set; }
        public int SiteId { get; set; }
        public int? AreaId { get; set; }

        public Area Area { get; set; }
        public VisitorBadge Badge { get; set; }
        public Staff SignedInBy { get; set; }
        public Site Site { get; set; }
        public Staff StaffEscort { get; set; }
        public Staff UnescortedApprovedBy { get; set; }
        public Visitor Visitor { get; set; }
    }
}

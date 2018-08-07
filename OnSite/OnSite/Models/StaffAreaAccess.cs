using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSite.Models
{
    public partial class StaffAreaAccess
    {
        [Display(Name="Staff Member")]
        public int StaffId { get; set; }
        [Display(Name="Area")]
        public int AreaId { get; set; }

        public Area Area { get; set; }
        public Staff Staff { get; set; }
    }
}

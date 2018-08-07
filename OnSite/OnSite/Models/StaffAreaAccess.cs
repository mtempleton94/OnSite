using System;
using System.Collections.Generic;

namespace OnSite.Models
{
    public partial class StaffAreaAccess
    {
        public int StaffId { get; set; }
        public int AreaId { get; set; }

        public Area Area { get; set; }
        public Staff Staff { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageStaffAreaAccess
{
    public class IndexModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public IndexModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public IList<StaffAreaAccess> StaffAreaAccess { get;set; }

        public async Task OnGetAsync()
        {
            StaffAreaAccess = await _context.StaffAreaAccess
                .Include(s => s.Area)
                .Include(s => s.Staff).ToListAsync();
        }
    }
}

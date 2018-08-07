using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageVisits
{
    public class IndexModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public IndexModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public IList<Visit> Visit { get;set; }

        public async Task OnGetAsync()
        {
            Visit = await _context.Visit
                .Include(v => v.Area)
                .Include(v => v.Badge)
                .Include(v => v.SignedInBy)
                .Include(v => v.Site)
                .Include(v => v.StaffEscort)
                .Include(v => v.UnescortedApprovedBy)
                .Include(v => v.Visitor).ToListAsync();
        }
    }
}

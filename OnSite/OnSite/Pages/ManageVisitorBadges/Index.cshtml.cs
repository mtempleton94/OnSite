using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageVisitorBadges
{
    public class IndexModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public IndexModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public IList<VisitorBadge> VisitorBadge { get;set; }

        public async Task OnGetAsync()
        {
            VisitorBadge = await _context.VisitorBadge
                .Include(v => v.Site).ToListAsync();
        }
    }
}

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
    public class DetailsModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DetailsModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public VisitorBadge VisitorBadge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VisitorBadge = await _context.VisitorBadge
                .Include(v => v.Site).SingleOrDefaultAsync(m => m.BadgeId == id);

            if (VisitorBadge == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

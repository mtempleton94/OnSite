using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageSites
{
    public class DetailsModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DetailsModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public Site Site { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Site = await _context.Site
                .Include(s => s.Organisation).SingleOrDefaultAsync(m => m.SiteId == id);

            if (Site == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

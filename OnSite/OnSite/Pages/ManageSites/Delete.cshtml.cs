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
    public class DeleteModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DeleteModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Site = await _context.Site.FindAsync(id);

            if (Site != null)
            {
                _context.Site.Remove(Site);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageSites
{
    public class EditModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public EditModel(OnSite.Models.VisitorContext context)
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
           ViewData["OrganisationId"] = new SelectList(_context.Organisation, "OrganisationId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(Site.SiteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SiteExists(int id)
        {
            return _context.Site.Any(e => e.SiteId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageVisitorBadges
{
    public class EditModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public EditModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["SiteId"] = new SelectList(_context.Site, "SiteId", "City");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(VisitorBadge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorBadgeExists(VisitorBadge.BadgeId))
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

        private bool VisitorBadgeExists(int id)
        {
            return _context.VisitorBadge.Any(e => e.BadgeId == id);
        }
    }
}

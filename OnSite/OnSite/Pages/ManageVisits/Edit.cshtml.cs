using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageVisits
{
    public class EditModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public EditModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visit Visit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visit = await _context.Visit
                .Include(v => v.Area)
                .Include(v => v.Badge)
                .Include(v => v.SignedInBy)
                .Include(v => v.Site)
                .Include(v => v.StaffEscort)
                .Include(v => v.UnescortedApprovedBy)
                .Include(v => v.Visitor).SingleOrDefaultAsync(m => m.VisitId == id);

            if (Visit == null)
            {
                return NotFound();
            }
           ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "Classification");
           ViewData["BadgeId"] = new SelectList(_context.VisitorBadge, "BadgeId", "BadgeType");
           ViewData["SignedInById"] = new SelectList(_context.Staff, "StaffId", "Email");
           ViewData["SiteId"] = new SelectList(_context.Site, "SiteId", "City");
           ViewData["StaffEscortId"] = new SelectList(_context.Staff, "StaffId", "Email");
           ViewData["UnescortedApprovedById"] = new SelectList(_context.Staff, "StaffId", "Email");
           ViewData["VisitorId"] = new SelectList(_context.Visitor, "VisitorId", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Visit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(Visit.VisitId))
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

        private bool VisitExists(int id)
        {
            return _context.Visit.Any(e => e.VisitId == id);
        }
    }
}

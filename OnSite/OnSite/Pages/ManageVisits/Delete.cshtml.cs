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
    public class DeleteModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DeleteModel(OnSite.Models.VisitorContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visit = await _context.Visit.FindAsync(id);

            if (Visit != null)
            {
                _context.Visit.Remove(Visit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

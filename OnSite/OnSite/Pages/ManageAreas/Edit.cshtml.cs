using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageAreas
{
    public class EditModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public EditModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Area Area { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Area = await _context.Area
                .Include(a => a.Site).SingleOrDefaultAsync(m => m.AreaId == id);

            if (Area == null)
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

            _context.Attach(Area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(Area.AreaId))
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

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.AreaId == id);
        }
    }
}

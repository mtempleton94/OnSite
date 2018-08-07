using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageStaffAreaAccess
{
    public class EditModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public EditModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StaffAreaAccess StaffAreaAccess { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffAreaAccess = await _context.StaffAreaAccess
                .Include(s => s.Area)
                .Include(s => s.Staff).SingleOrDefaultAsync(m => m.StaffId == id);

            if (StaffAreaAccess == null)
            {
                return NotFound();
            }
           ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "Classification");
           ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StaffAreaAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffAreaAccessExists(StaffAreaAccess.StaffId))
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

        private bool StaffAreaAccessExists(int id)
        {
            return _context.StaffAreaAccess.Any(e => e.StaffId == id);
        }
    }
}

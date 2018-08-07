using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageStaffAreaAccess
{
    public class DeleteModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DeleteModel(OnSite.Models.VisitorContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffAreaAccess = await _context.StaffAreaAccess.FindAsync(id);

            if (StaffAreaAccess != null)
            {
                _context.StaffAreaAccess.Remove(StaffAreaAccess);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

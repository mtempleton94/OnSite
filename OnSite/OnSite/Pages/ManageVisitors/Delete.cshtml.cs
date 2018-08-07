using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageVisitors
{
    public class DeleteModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DeleteModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visitor Visitor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visitor = await _context.Visitor
                .Include(v => v.Organisation).SingleOrDefaultAsync(m => m.VisitorId == id);

            if (Visitor == null)
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

            Visitor = await _context.Visitor.FindAsync(id);

            if (Visitor != null)
            {
                _context.Visitor.Remove(Visitor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageOrganisations
{
    public class DeleteModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DeleteModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Organisation Organisation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Organisation = await _context.Organisation.SingleOrDefaultAsync(m => m.OrganisationId == id);

            if (Organisation == null)
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

            Organisation = await _context.Organisation.FindAsync(id);

            if (Organisation != null)
            {
                _context.Organisation.Remove(Organisation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

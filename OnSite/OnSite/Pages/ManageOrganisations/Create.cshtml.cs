using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSite.Models;

namespace OnSite.Pages.ManageOrganisations
{
    public class CreateModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public CreateModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Organisation Organisation { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Organisation.Add(Organisation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
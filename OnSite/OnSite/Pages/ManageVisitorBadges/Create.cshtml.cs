using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSite.Models;

namespace OnSite.Pages.ManageVisitorBadges
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
        ViewData["SiteId"] = new SelectList(_context.Site, "SiteId", "City");
            return Page();
        }

        [BindProperty]
        public VisitorBadge VisitorBadge { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VisitorBadge.Add(VisitorBadge);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
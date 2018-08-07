using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSite.Models;

namespace OnSite.Pages.ManageVisits
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
        ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "Classification");
        ViewData["BadgeId"] = new SelectList(_context.VisitorBadge, "BadgeId", "BadgeType");
        ViewData["SignedInById"] = new SelectList(_context.Staff, "StaffId", "Email");
        ViewData["SiteId"] = new SelectList(_context.Site, "SiteId", "City");
        ViewData["StaffEscortId"] = new SelectList(_context.Staff, "StaffId", "Email");
        ViewData["UnescortedApprovedById"] = new SelectList(_context.Staff, "StaffId", "Email");
        ViewData["VisitorId"] = new SelectList(_context.Visitor, "VisitorId", "FirstName");
            return Page();
        }

        [BindProperty]
        public Visit Visit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Visit.Add(Visit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
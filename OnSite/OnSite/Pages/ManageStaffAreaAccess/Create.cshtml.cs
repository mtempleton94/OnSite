using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSite.Models;

namespace OnSite.Pages.ManageStaffAreaAccess
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
        ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email");
            return Page();
        }

        [BindProperty]
        public StaffAreaAccess StaffAreaAccess { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StaffAreaAccess.Add(StaffAreaAccess);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
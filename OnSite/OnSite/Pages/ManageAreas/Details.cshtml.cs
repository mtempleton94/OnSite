using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnSite.Models;

namespace OnSite.Pages.ManageAreas
{
    public class DetailsModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public DetailsModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}

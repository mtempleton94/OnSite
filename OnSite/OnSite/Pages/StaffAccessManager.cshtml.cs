using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnSite.Pages
{
    public class StaffAccessManagerModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public StaffAccessManagerModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}





 
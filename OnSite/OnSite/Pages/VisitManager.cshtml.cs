using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnSite.Models;

namespace OnSite.Pages
{
    public class VisitManagerModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public VisitManagerModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visit Visit { get; set; }

        [BindProperty]
        public Visitor Visitor { get; set; }

        [BindProperty]
        public Organisation Organisation { get; set; }

        public IList<Visit> OnSiteVisitors { get; set; }
        public IList<Visit> UnapprovedVisits { get; set; }

        //=====================================================================
        // Page Load - Get a list of all approved visits
        //=====================================================================
        public IActionResult OnGetAsync()
        {
            // create list of approved visits
            IQueryable<Visit> onSiteVisitors =
                from visit in _context.Visit
                where visit.ApprovalStatus == true &&
                visit.SignOutTime == null
                select visit;
            OnSiteVisitors = onSiteVisitors.ToList();

            // create list of unapproved visits
            IQueryable<Visit> unapprovedVisits =
                from visit in _context.Visit
                where visit.ApprovalStatus == false
                select visit;
            UnapprovedVisits = unapprovedVisits.ToList();

            return Page();
        }

        //=====================================================================
        // Get Visitor Name based on Visit Id
        //=====================================================================
        public string GetVisitorName(int VisitId)
        {
            string visitorName =
                (from visitor in _context.Visitor
                 join visit in _context.Visit on visitor.VisitorId equals visit.VisitorId into Visit_Visitor
                 from joinVisitor in Visit_Visitor.DefaultIfEmpty()
                 where joinVisitor.VisitId == VisitId
                 select visitor.FirstName).SingleOrDefault();
            return visitorName;
        }
    }
}


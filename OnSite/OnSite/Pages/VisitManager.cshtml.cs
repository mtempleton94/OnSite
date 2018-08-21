using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnSite.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public IList<Organisation> OrganisationList { get; set; }

        //=====================================================================
        // Page Load - Get a list of all approved visits
        //=====================================================================
        public async Task OnGetAsync()
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

            // get list of all organisations
            OrganisationList = await _context.Organisation.ToListAsync();
        }

        //=====================================================================
        // Get Visitor Name based on Visit Id
        //=====================================================================
        public Visitor GetVisitor(int VisitId)
        {
            IQueryable<Visitor> visitorQuery =
                 (from visitor in _context.Visitor
                  join visit in _context.Visit on visitor.VisitorId equals visit.VisitorId into Visit_Visitor
                  from joinVisitor in Visit_Visitor.DefaultIfEmpty()
                  where joinVisitor.VisitId == VisitId
                  select visitor);
            return visitorQuery.ToArray().First();
        }

        //=====================================================================
        // Get Visit Data as a JSON Object
        //=====================================================================
        public JsonResult OnGetVisitData(int visitId)
        {
            IQueryable<Visit> visitData =
                from visit in _context.Visit
                where (visit.VisitId == visitId)
                select visit;

            string jsonData = JsonConvert.SerializeObject(visitData);
            return new JsonResult(jsonData);
        }

        //=====================================================================
        // Get Visitor Data as a JSON Object
        //=====================================================================
        public JsonResult OnGetVisitorData(int visitId)
        {
            IQueryable<Visitor> visitorData =
                 (from visitor in _context.Visitor
                  join visit in _context.Visit on visitor.VisitorId equals visit.VisitorId into Visit_Visitor
                  from joinVisitor in Visit_Visitor.DefaultIfEmpty()
                  where joinVisitor.VisitId == visitId
                  select visitor);

            string jsonData = JsonConvert.SerializeObject(visitorData);
            return new JsonResult(jsonData);
        }


    }
}


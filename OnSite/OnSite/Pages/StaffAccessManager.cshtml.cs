using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnSite.Models;

namespace OnSite.Pages
{
    // class to store data for organisations and sites
    public class OrganisationSiteModel
    {
        public int OrgIDView { get; set; }
        public int SiteIDView { get; set; }
        public string OrgNameView { get; set; }
        public string SiteStreetAddressView { get; set; }
        public string SiteCityView { get; set; }
        public string SiteStateView { get; set; }
        public string SitePostCodeView { get; set; }
    }

    public class StaffAccessManagerModel : PageModel
    {
        [BindProperty]
        public StaffAreaAccess StaffAreaAccess { get; set; }

        // store sites and organisations
        public IList<Site> SiteList { get; set; }
        public IList<Organisation> OrganisationList { get; set; }

        // store list of all sites and associated organisation
        public IList<OrganisationSiteModel> OrganisationSiteList { get; set; }

        // store list of staff members
        public IList<Staff> StaffList { get; set; }

        // store context
        private readonly OnSite.Models.VisitorContext _context;

        // set up model context
        public StaffAccessManagerModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        //=====================================================================
        // Page Load
        //=====================================================================
        public IActionResult OnGet()
        {
            return Page();
        }

        //=====================================================================
        // Get list of all staff members
        //=====================================================================
        public IList<Staff> GetAllStaff()
        {
            // select all staff
            IQueryable<Staff> staffMembers =
            from staff in _context.Staff
            select staff;

            // convert to list and return
            StaffList = staffMembers.ToList();
            return StaffList;
        }

        //=====================================================================
        // Get list of staff members based on search criteria
        //=====================================================================
        public JsonResult OnGetStaffSearch(String SearchString)
        {
            IQueryable<Staff> searchStaff;
            if (SearchString == null)
            {
                // select all staff
                searchStaff =
                    from staff in _context.Staff
                select staff;
            } else {
                // select staff matching search criteria
                searchStaff =
                    from staff in _context.Staff
                    where (SearchString != null)
                    && (staff.FirstName.Contains(SearchString)
                    || staff.LastName.Contains(SearchString)
                    || staff.Position.Contains(SearchString)
                    || staff.Phone.Contains(SearchString)
                    || staff.Email.Contains(SearchString))
                select staff;
            }

            // convert results to json and return
            string jsonData = JsonConvert.SerializeObject(searchStaff);
            return new JsonResult(jsonData);
        }

        //=====================================================================
        // Get list of all sites and associated organisation
        //=====================================================================
        public IList<OrganisationSiteModel> GetOrganisationSites ()
        {
            SiteList = _context.Site.ToList();
            OrganisationList = _context.Organisation.ToList();

            // generate a list of sites based on organisation
            IQueryable<OrganisationSiteModel> orgSites =
                from org in _context.Organisation
                join site in _context.Site on org.OrganisationId equals site.OrganisationId into Details
                from orgSite in Details.DefaultIfEmpty()
                where orgSite.SiteId > 0
                select new OrganisationSiteModel
                {
                    OrgIDView = org.OrganisationId,
                    SiteIDView = orgSite.SiteId,
                    OrgNameView = org.Name,
                    SiteStreetAddressView = orgSite.StreetAddress,
                    SiteCityView = orgSite.City,
                    SiteStateView = orgSite.State,
                    SitePostCodeView = orgSite.PostCode,
                };

            OrganisationSiteList = orgSites.ToList();
            return OrganisationSiteList;
        }

        //=====================================================================
        // Save Updates to Areas Staff Member can access
        //=====================================================================
        public ActionResult OnPostStaffAccessUpdate(int id)
        {
            string jsonData = JsonConvert.SerializeObject("[TODO] Area Access Update");
            return new JsonResult(jsonData);
        }
    }
}

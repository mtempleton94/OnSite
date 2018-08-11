using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnSite.Models;

namespace OnSite.Pages
{
    //=========================================================================
    // Store site information for display following join
    // and query of organisation and site tables
    //=========================================================================
    public class OrganisationSiteViewModel
    {
        public int OrgIDView { get; set; }
        public int SiteIDView { get; set; }
        public string OrgNameView { get; set; }
        public string SiteStreetAddressView { get; set; }
    }

    public class SignInModel : PageModel
    {
        private readonly OnSite.Models.VisitorContext _context;

        public SignInModel(OnSite.Models.VisitorContext context)
        {
            _context = context;
        }

        // list of Sites and organisations
        public IList<Site> SiteList { get; set; }
        public IList<Organisation> OrganisationList { get; set; }
        public IList<OrganisationSiteViewModel> OrgSiteList { get; set; }
        public IList<Area> SiteAreaList { get; set; }
        public SelectList OrgSites;

        //=====================================================================
        // Page load
        //=====================================================================
        public async Task OnGetAsync()
        {

            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "Classification");
            ViewData["VisitorId"] = new SelectList(_context.Visitor, "VisitorId", "FirstName");
            

            SiteList = await _context.Site.ToListAsync();
            OrganisationList = await _context.Organisation.ToListAsync();

            // generate a list of sites based on organisation
            IQueryable<OrganisationSiteViewModel> orgSites = 
                from org in _context.Organisation
                join site in _context.Site on org.OrganisationId equals site.OrganisationId into Details
                from m in Details.DefaultIfEmpty()
                where m.SiteId > 0
                select new OrganisationSiteViewModel
                {
                    OrgIDView = org.OrganisationId,
                    SiteIDView = m.SiteId,
                    OrgNameView = org.Name,
                    SiteStreetAddressView = m.StreetAddress                 
                };

            // display the organisation names when selecting organisation id
            ViewData["SiteId"] = new SelectList(_context.Site, "SiteId", "City");
            ViewData["OrganisationId"] = new SelectList(_context.Organisation, "OrganisationId", "Name");

            OrgSiteList = orgSites.ToList();
            
        }

        //=====================================================================
        // Display list of areas associated with site
        //=====================================================================
        public JsonResult OnGetAreas(int OrgId, int SiteId)
        {           
            IQueryable<Area> siteAreas = 
                from area in _context.Area
                where area.SiteId == SiteId
                select new Area
                {
                    AreaId = area.AreaId,
                    Floor = area.Floor,
                    Description = area.Description,
                    Classification = area.Classification,
                    SiteId = area.SiteId
                };

            // convert results to json and return
            string jsonData = JsonConvert.SerializeObject(siteAreas);
            return new JsonResult(jsonData);

        }

        //=====================================================================
        // Display list of previous visitors based on search criteria
        //=====================================================================
        public JsonResult OnGetVisitors(String FirstName, String LastName,
            String IdentificationNumber)
        {
            IQueryable<Visitor> searchVisitors =
                from visitor in _context.Visitor
                where (FirstName != null || LastName != null || IdentificationNumber != null)
                && (FirstName == null || visitor.FirstName.Contains(FirstName))
                && (LastName == null || visitor.LastName.Contains(LastName))
                && (IdentificationNumber == null || visitor.IdentificationNumber.Contains(IdentificationNumber))
                select new Visitor
                {
                    VisitorId = visitor.VisitorId,
                    FirstName = visitor.FirstName,
                    LastName = visitor.LastName,
                    OrganisationId = visitor.OrganisationId,
                    IdentificationNumber = visitor.IdentificationNumber,
                };

            // convert results to json and return
            string jsonData = JsonConvert.SerializeObject(searchVisitors);
            return new JsonResult(jsonData);

        }

        //=====================================================================
        // Get the name of an organisation based on its ID
        // CURRENTLY JUST PLACEHOLDER TO RETURN A VALUE
        //=====================================================================
        public ActionResult OnGetOrganisationName(int OrganisationId)
        {
            string test = "US Government";
            return Content(test);
        }

        //=====================================================================
        // Submit visitor information
        //=====================================================================
        [BindProperty]
        public Visit Visit { get; set; }
        public Visitor Visitor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Visit.Add(Visit);
            //_context.Visitor.Add(Visitor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./SignIn");
        }
    }
}


using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class OrganisationAdminController : BaseController
    {

        private IOrganisationAdminDataProvider _iOrganisationAdminDataProvider;

        // GET: OrganisationAdmin
        //[CustomAuthorize(Permissions = Constants.Can_Access_Orgadmin, PermissionType = Constants.Can_View)]
        public ActionResult OrganisationList()
        {
            _iOrganisationAdminDataProvider = new OrganisationAdminDataProvider();
            OrganisationListModel organisationlistModel = _iOrganisationAdminDataProvider.OrganisationList();
            return View(organisationlistModel);
        }
       // [CustomAuthorize(Permissions = Constants.Can_Access_Company, PermissionType = Constants.Can_CreateOrUpdate)]
        public ActionResult Company(string id)
        {
            _iOrganisationAdminDataProvider = new OrganisationAdminDataProvider();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            ServiceResponse response = _iOrganisationAdminDataProvider.GetCompany(ClientID);
            return View(response.Data);

        }
        [HttpPost]
        public ActionResult SaveCompany(CompanyAdminModel companies)
        {
            _iOrganisationAdminDataProvider = new OrganisationAdminDataProvider();
            var response = new ServiceResponse();
            response = _iOrganisationAdminDataProvider.SaveCompany(companies);
            return Json(response);
        }
    }
}
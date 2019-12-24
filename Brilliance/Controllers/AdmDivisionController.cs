using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class AdmDivisionController : BaseController
    {
        private IAdmDivisionDataProvider _idivisionDataProvider;

        // GET: AdmDivision
        public ActionResult Division(string clientid, string companyid)
        {
            _idivisionDataProvider = new AdmDivisionDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(clientid);
            Guid CompanyID = Common.CheckIdNullOrEmpty(companyid);
            response = _idivisionDataProvider.GetDivision(ClientID, CompanyID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveDivision(AdmDivisionViewModel divisionModel)
        {
            _idivisionDataProvider = new AdmDivisionDataProvider();
            ServiceResponse response = _idivisionDataProvider.SaveDivision(divisionModel);
            return Json(response);
        }

        //[CustomAuthorize(Permissions = Constants.Can_Access_Pms_Requisition, PermissionType = Constants.Can_Delete)]
        public ActionResult DeleteDivision(string DeleteID)
        {
            _idivisionDataProvider = new AdmDivisionDataProvider();
            Guid DivisionID = Common.CheckIdNullOrEmpty(DeleteID);
            ServiceResponse response = _idivisionDataProvider.DeleteDivision(DivisionID);
            return Json(response);
        }
    }
}
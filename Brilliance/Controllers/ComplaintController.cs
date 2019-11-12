using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class ComplaintController : Controller
    {
        private IWebsubmissionDataProvider _iwebsubmissionDataProvider;
        private IComplaintDataProvider _icomplaintDataProvider;

        public ActionResult Websubmission()
        {
            _iwebsubmissionDataProvider = new WebsubmissionDataProvider();
            var response = new ServiceResponse();
            response = _iwebsubmissionDataProvider.GetComplaint();
            return View(response.Data);
        }
        public ActionResult GetDivisionByCompany(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetDivisionByCompany(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveComplaint(ComplaintViewModel Complaintmodel)
        {
            _iwebsubmissionDataProvider = new WebsubmissionDataProvider();
            var response = new ServiceResponse();
            response = _iwebsubmissionDataProvider.SaveComplaints(Complaintmodel);
            return Json(response);
        }
    }
}
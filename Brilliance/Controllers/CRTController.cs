using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class CRTController : BaseController
    {
        private IComplaintDataProvider  _icomplaintDataProvider;
        // GET: CRT
        public ActionResult CRTList()
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            ComplaintListModel complaintlistModel = _icomplaintDataProvider.ComplaintList();
            return View(complaintlistModel);
        }
        public ActionResult CRT(string id)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();
            Guid ComplaintID = Common.CheckIdNullOrEmpty(id);
            response = _icomplaintDataProvider.GetComplaint(ComplaintID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveComplaint(ComplaintViewModel Complaintmodel)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();
            response = _icomplaintDataProvider.SaveComplaints(Complaintmodel);
            return Json(response);
        }
        public ActionResult DeleteComplaint(string DeleteID)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();
            Guid ComplaintID = Common.CheckIdNullOrEmpty(DeleteID);
            response = _icomplaintDataProvider.DeleteComplaint(ComplaintID);
            return Json(response);
        }
        public ActionResult GetDivisionByCompany(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetDivisionByCompany(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductByCompany(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetProductByCompany(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
    }
}
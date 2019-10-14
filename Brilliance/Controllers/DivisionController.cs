using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using Brilliance.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Brilliance.Controllers
{
    public class DivisionController : Controller
    {
        private IDivisionDataProvider _idivisionDataProvider;

        // GET: Division
        public ActionResult DivisionList()
        {
            _idivisionDataProvider = new DivisionDataProvider();
            DivisionListModel clientlistModel = _idivisionDataProvider.DivisionList();
            return View(clientlistModel);
        }
        public ActionResult Division(string id)
        {
            _idivisionDataProvider = new DivisionDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _idivisionDataProvider.GetDivision(ClientID);
            return View(response.Data);

        }
        [HttpPost]
        public ActionResult SaveDivision(DivisionViewModel Divisionmodel)
        {
            _idivisionDataProvider = new DivisionDataProvider();
            var response = new ServiceResponse();
            response = _idivisionDataProvider.SaveDivision(Divisionmodel);
            return Json(response);
        }
        public ActionResult DeleteDivision(string DeleteID)
        {
            _idivisionDataProvider = new DivisionDataProvider();
            var response = new ServiceResponse();
            Guid ClientId = Common.CheckIdNullOrEmpty(DeleteID);
            response = _idivisionDataProvider.DeleteDivision(ClientId);
            return Json(response);
        }
        public ActionResult GetCompanyByClient(string cid)
        {
            _idivisionDataProvider = new DivisionDataProvider();
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _idivisionDataProvider.GetCompanyByClient(ClientID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
    }
}
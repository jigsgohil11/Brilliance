using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class AdmCrtController : BaseController
    {
        private IAdmCrtDataProvider _iadmcrtDataProvider;

        // GET: AdmCrt
        public ActionResult CRTadmin()
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.GetCrtSetup();
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult Savelabelconfig(CrtAdminViewmodel CRTAdminVM)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Savelabelconfig(CRTAdminVM);
            return Json(response);
        }
        public ActionResult LabelconfigList(Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            CrtAdminViewmodel labellist = _iadmcrtDataProvider.labelconfiglist(ClientID);
            return PartialView("~/Views/AdmCrt/_LabelConfigList.cshtml", labellist);
        }
        public ActionResult EditLabelConfig(Guid LabelconfigID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            CrtAdminViewmodel crtadminVM = _iadmcrtDataProvider.EditLabelConfig(LabelconfigID);
            return PartialView("~/Views/AdmCrt/_LabelConfiguration.cshtml", crtadminVM);
        }
        public ActionResult DropselectList(Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            DropSelectViewmodel model = _iadmcrtDataProvider.DropselectgList(ClientID);
            return PartialView("~/Views/AdmCrt/_DropselectConfiguration.cshtml", model);
        }
        public ActionResult AddDropSelect(string Category,Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            DropselectModel model = _iadmcrtDataProvider.AddDropSelect(Category, ClientID);
            return PartialView("~/Views/AdmCrt/_AddNewDropSelectItem.cshtml", model);
        }
        [HttpPost]
        public ActionResult Savedropselectconfig(Guid ClientID,Guid? Refid,Guid? Refid1,string name,string desc,string category)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Savedropselectconfig(ClientID,Refid,Refid1,name,desc,category);
            return Json(response);
        }
    }
}
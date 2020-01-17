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
    }
}
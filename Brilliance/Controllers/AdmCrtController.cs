using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;
using Brilliance.Models.Entity;

namespace Brilliance.Controllers
{
    public class AdmCrtController : BaseController
    {
        private IAdmCrtDataProvider _iadmcrtDataProvider;

        // GET: AdmCrtWWA@
        public ActionResult CRTadmin()
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.GetCrtSetup();
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Savelabelconfig(string InstanceName, string Tier2, string incidentdate, string Tier3, string policystatus,
                                            string Accnumber, string Rootcause, string Idnumber, string howcomreceived, string contactno,
                                            string Compregulatory, string emailaddress, string feedbackregulatory, string productcategory, string Overalloutcome,
                                            string Producttype, string Compensation, string Compcategory, string Regulatedcost, string Nature,
                                            string Value, string TCF, string DissatisfactionLevel, string SatisfactionResolution, bool? IsTemplate, bool? IsEdit, Guid? ClientID, Guid? LabelconfigID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Savelabelconfig(InstanceName, Tier2, incidentdate, Tier3, policystatus, Accnumber, Rootcause, Idnumber, howcomreceived, contactno,
                                            Compregulatory, emailaddress, feedbackregulatory, productcategory, Overalloutcome, Producttype, Compensation, Compcategory,
                                            Regulatedcost, Nature, Value, TCF, DissatisfactionLevel, SatisfactionResolution, IsTemplate, IsEdit, ClientID, LabelconfigID);
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
        public ActionResult AddDropSelect(string Category, Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            DropselectModel model = _iadmcrtDataProvider.AddDropSelect(Category, ClientID);
            return PartialView("~/Views/AdmCrt/_AddNewDropSelectItem.cshtml", model);
        }
        [HttpPost]
        public ActionResult Savedropselectconfig(Guid TermId, Guid ClientID, Guid? Refid, Guid? Refid1, string name, string desc, string category, bool isedit)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Savedropselectconfig(TermId, ClientID, Refid, Refid1, name, desc, category, isedit);
            return Json(response);
        }
        public ActionResult Deletedropselectconfig(Guid TermId)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Deletedropselectconfig(TermId);
            return Json(response);
        }
        [HttpPost]
        public ActionResult SaveCRTconfig(CrtAdminViewmodel crtadminVM)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.SaveCRTconfig(crtadminVM);
            return Json(response);
        }
    }
}
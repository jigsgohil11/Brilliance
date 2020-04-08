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

        // GET: AdmCrt
        public ActionResult CRTadmin(string id)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(id);
            response = _iadmcrtDataProvider.GetCrtSetup(ClientID);
            return View(response.Data);
        }
        public ActionResult GetLabelConfig(Guid TemplateID,Guid ClientID,string OrgName, bool? IsEdit)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            CrtAdminViewmodel crtadminVM = _iadmcrtDataProvider.GetLabelConfig(TemplateID, ClientID, OrgName, IsEdit);
            return PartialView("~/Views/AdmCrt/_LabelConfiguration.cshtml", crtadminVM);    
        }

        [HttpPost]
        public ActionResult Savelabelconfig(string TemplateName, string Tier2, string incidentdate, string Tier3, string policystatus,
                                            string Accnumber, string Rootcause, string Idnumber, string howcomreceived, string contactno,
                                            string Compregulatory, string emailaddress, string feedbackregulatory, string productcategory, string Overalloutcome,
                                            string Producttype, string Compensation, string Compcategory, string Regulatedcost, string Nature,
                                            string Value, string TCF, string DissatisfactionLevel, string SatisfactionResolution, bool? IsTemplate, bool? IsEdit, Guid? ClientID, Guid? RefTempID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.Savelabelconfig(TemplateName, Tier2, incidentdate, Tier3, policystatus, Accnumber, Rootcause, Idnumber, howcomreceived, contactno,
                                            Compregulatory, emailaddress, feedbackregulatory, productcategory, Overalloutcome, Producttype, Compensation, Compcategory,
                                            Regulatedcost, Nature, Value, TCF, DissatisfactionLevel, SatisfactionResolution, IsTemplate, IsEdit, ClientID, RefTempID);
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
        public ActionResult NotificationwordingList(Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            DropSelectViewmodel model = null;
            return PartialView("~/Views/AdmCrt/_Notificationconfiguration.cshtml", model);
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
        [HttpPost]
        public ActionResult SavedropselectInTemplate(Guid ClientID,Guid TemplateID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            response = _iadmcrtDataProvider.SavedropselectInTemplate(ClientID, TemplateID);
            return Json(response);
        }
        public ActionResult GetComplaintReasonList(Guid ComplaintTypeID,Guid ClientID)
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            //Guid ComplaintCategoryID = Common.CheckIdNullOrEmptyNonEncrypt(id);
            ServiceResponse response = _iadmcrtDataProvider.GetComplaintReasonList(ComplaintTypeID, ClientID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTemplateList()
        {
            _iadmcrtDataProvider = new AdmCrtDataProvider();
            var response = new ServiceResponse();
            CrtAdminViewmodel crm = _iadmcrtDataProvider.GetTemplateList();
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
    }
}
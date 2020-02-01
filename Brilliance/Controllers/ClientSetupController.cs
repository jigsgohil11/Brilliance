using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace Brilliance.Controllers
{
    public class ClientSetupController : BaseController
    {
        private IClientSetupDataProvider _iclientsetupDataProvider;

        // GET: ClientSetup
        public ActionResult Client(string id)
        {
            _iclientsetupDataProvider = new ClientSetupDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _iclientsetupDataProvider.GetClient(ClientID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveClient(ClientSetupViewModel Clients, HttpPostedFileBase orglogo)
        {
            _iclientsetupDataProvider = new ClientSetupDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Guid.NewGuid();
            if (Clients != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Clients.client.ClientID)) && Clients.client.ClientID != Guid.Empty)
                {
                    ClientID = Clients.client.ClientID;
                    Clients.client.IsEdit = true;
                }
                else
                {
                    Clients.client.IsEdit = false;
                    Clients.client.ClientID = ClientID;

                }

                if (orglogo != null)
                {
                    var ImageNameToSave = ClientID + Path.GetExtension(orglogo.FileName);
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Organisationlogos/")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Organisationlogos/"));

                    //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + FilePath)))
                    //    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + FilePath));

                    if (Clients.client.Logo != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Organisationlogos/"), Clients.client.Logo)))

                        {
                            System.IO.File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Organisationlogos/"), Clients.client.Logo));
                        }
                    }
                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Organisationlogos/"), ImageNameToSave);
                    orglogo.SaveAs(path);

                }
                response = _iclientsetupDataProvider.SaveClient(Clients, orglogo);
            }
            TempData["Success"] = response.Message;
            return RedirectToAction("OrganisationList", "OrganisationAdmin");
            //return RedirectToAction("InstanceSubscription", "ClientSetup");
            //return Json(response);
        }

        public ActionResult InstanceSubscription(string id)
        {
            _iclientsetupDataProvider = new ClientSetupDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _iclientsetupDataProvider.Getsubscription(ClientID);
            return View(response.Data);
        }

        public ActionResult GetInstanceDetail(Guid ClientID)
        {
            _iclientsetupDataProvider = new ClientSetupDataProvider();
            var response = new ServiceResponse();
            // Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _iclientsetupDataProvider.GetInstanceDetail(ClientID);
            return PartialView("~/Views/ClientSetup/_AddInstance.cshtml", response.Data);
        }
        [HttpPost]
        public ActionResult SaveInstance(AppSubscriptionViewModel AppModel)
        {
            _iclientsetupDataProvider = new ClientSetupDataProvider();
            var response = new ServiceResponse();
            response = _iclientsetupDataProvider.SaveInstance(AppModel);
            return Json(response);
        }
    }
}
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
    public class OrganizationController : BaseController
    {
        private IClientDataProvider _iclientDataProvider;
       // GET: Organization

        public ActionResult OrganizationList()
        {
            _iclientDataProvider = new ClientDataProvider();
            ClientListModel clientlistModel = _iclientDataProvider.ClientList();
            return View(clientlistModel);
          
        }
        public ActionResult Organization(string id)
        {
            _iclientDataProvider = new ClientDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _iclientDataProvider.GetClient(ClientID);
            return View(response.Data);

        }
        [HttpPost]
        public ActionResult SaveClient(ClientViewModel clientViewModel)
        {
            _iclientDataProvider = new ClientDataProvider();
            var response = new ServiceResponse();   
            response = _iclientDataProvider.SaveClients(clientViewModel);
            return Json(response);
        }
        public ActionResult DeleteClient(string DeleteID)
        {
            _iclientDataProvider = new ClientDataProvider();
            var response = new ServiceResponse();
            Guid ClientId = Common.CheckIdNullOrEmpty(DeleteID);
            response = _iclientDataProvider.DeleteClient(ClientId);
            return Json(response);
        }
    }
}
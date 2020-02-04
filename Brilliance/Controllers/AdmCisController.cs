using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class AdmCisController : Controller
    {
        //private IAdmCrtDataProvider _iadmcrtDataProvider;

        // GET: AdmCis
        public ActionResult CISadmin()
        {
            //_iadmcrtDataProvider = new AdmCrtDataProvider();
            //var response = new ServiceResponse();
            //response = _iadmcrtDataProvider.GetCrtSetup();
            return View();
        }
    }
}
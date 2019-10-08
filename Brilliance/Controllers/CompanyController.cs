using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult CompanyList()
        {
            return View();
        }
        public ActionResult Company()
        {
            return View();
        }
    }
}
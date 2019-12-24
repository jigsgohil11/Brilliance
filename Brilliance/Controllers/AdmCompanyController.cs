using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class AdmCompanyController : BaseController
    {
        private IAdmCompanyDataProvider _icompanyDataProvider;

        // GET: AdmCompany
        public ActionResult Company(string id)
        {
            _icompanyDataProvider = new AdmCompanyDataProvider();
            var response = new ServiceResponse();
            Guid ClientID = Common.CheckIdNullOrEmpty(id);
            response = _icompanyDataProvider.GetCompany(ClientID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveCompany(AdmCompanyViewModel companyModel)
        {
            _icompanyDataProvider = new AdmCompanyDataProvider();
            ServiceResponse response = _icompanyDataProvider.SaveCompany(companyModel);
            return Json(response);
        }
        public ActionResult DeleteCompany(Guid companyid)
        {
            _icompanyDataProvider = new AdmCompanyDataProvider();
            ServiceResponse response = _icompanyDataProvider.DeleteCompany(companyid);
            return Json(response);
        }

    }
}
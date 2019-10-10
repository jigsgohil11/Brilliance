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
    public class CompanyController : BaseController
    {
        private ICompanyDataProvider _icompanyDataProvider;
        // GET: Company
        public ActionResult CompanyList()
        {
            _icompanyDataProvider = new CompanyDataProvider();
            CompanyListModel companylistModel = _icompanyDataProvider.CompanyList();
            return View(companylistModel);
        }
        public ActionResult Company(string id)
        {
            _icompanyDataProvider = new CompanyDataProvider();
            var response = new ServiceResponse();
            Guid CompanyID = Common.CheckIdNullOrEmpty(id);
            response = _icompanyDataProvider.GetCompany(CompanyID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveCompany(CompanyViewModel companyViewModel)
        {
            _icompanyDataProvider = new CompanyDataProvider();
            var response = new ServiceResponse();
            response = _icompanyDataProvider.SaveCompanies(companyViewModel);
            return Json(response);
        }
        public ActionResult DeleteCompany(string DeleteID)
        {
            _icompanyDataProvider = new CompanyDataProvider();
            var response = new ServiceResponse();
            Guid CompanyID = Common.CheckIdNullOrEmpty(DeleteID);
            response = _icompanyDataProvider.DeleteCompany(CompanyID);
            return Json(response);
        }
    }
}
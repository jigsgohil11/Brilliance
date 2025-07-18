﻿using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
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
        public ActionResult GetSectorByIndustry(string id)
        {
            _icompanyDataProvider = new CompanyDataProvider();
            Guid IndustryID = Common.CheckIdNullOrEmptyNonEncrypt(id);
            ServiceResponse response = _icompanyDataProvider.GetSectorByIndustry(IndustryID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
    }
}
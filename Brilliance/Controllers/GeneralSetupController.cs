using Brilliance.Infrastructure;
using Brilliance.Models;
using Brilliance.Controllers;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class GeneralSetupController : BaseController
    {
        private IGeneralSetupDataProvider _generalSetupDataProvider;

        // GET: GeneralSetup
        //[CustomAuthorize(Permissions = Constants.Can_Access_GeneralSetup, PermissionType = Constants.Can_View)]
        public ActionResult ProjectTermListbyCategory(string categoryid)
        {

            _generalSetupDataProvider = new GeneralSetupDataProvider();
            Guid CategoryID = Common.CheckIdNullOrEmpty(categoryid);
            ProjectTermListByCategory ProjectTermList = _generalSetupDataProvider.TermlistbyCategory();
            if (CategoryID != null && CategoryID != Guid.Empty)
            {
                foreach (var projectTermList in ProjectTermList.CategoryList)
                {
                    if (projectTermList.ProjectTermCategoryID == CategoryID)
                    {
                        projectTermList.IsSelectedCategory = true;
                    }
                    else
                    {
                        projectTermList.IsSelectedCategory = false;
                    }
                }
            }
            return View(ProjectTermList);
        }

        public ActionResult CategoryList(string Categoryid)
        {
            _generalSetupDataProvider = new GeneralSetupDataProvider();
            Guid ProjectTermCategoryID = Common.CheckIdNullOrEmpty(Categoryid);
            ProjectTermListByCategory ProjectTermList = _generalSetupDataProvider.CategoryList(ProjectTermCategoryID);
            return PartialView("~/Views/GeneralSetup/_CategoryList.cshtml", ProjectTermList);
        }


        public ActionResult GeneralsetupPopup(Guid Categoryid, string CategoryName)
        {
            _generalSetupDataProvider = new GeneralSetupDataProvider();
            ProjectTermListByCategory ProjectTermList = _generalSetupDataProvider.AddGeneralsetupPopup(Categoryid, CategoryName);
            return PartialView("~/Views/GeneralSetup/_GeneralSetup.cshtml", ProjectTermList);
        }

        public ActionResult GeneralsetupEditPopup(string projecttermid)
        {
            _generalSetupDataProvider = new GeneralSetupDataProvider();
            Guid ProjectTermID = Common.CheckIdNullOrEmpty(projecttermid);
            ProjectTermListByCategory ProjectTermList = _generalSetupDataProvider.GeneralsetupPopup(ProjectTermID);
            return PartialView("~/Views/GeneralSetup/_GeneralSetup.cshtml", ProjectTermList);
        }

        [HttpPost]
        public ActionResult SaveGeneralSetup(ProjectTermListByCategory ProjectCategoryModel)
        {
            _generalSetupDataProvider = new GeneralSetupDataProvider();
            var response = new ServiceResponse();
            response = _generalSetupDataProvider.SaveGeneralSetup1(ProjectCategoryModel);
            return Json(response);
        }

        //[CustomAuthorize(Permissions = Constants.Can_Access_GeneralSetup, PermissionType = Constants.Can_Delete)]
        public ActionResult DeleteSetup(string DeleteID)
        {
            _generalSetupDataProvider = new GeneralSetupDataProvider();
            var response = new ServiceResponse();
            Guid ProjectTermID = Common.CheckIdNullOrEmpty(DeleteID);
            response = _generalSetupDataProvider.DeleteSetup(ProjectTermID);
            return Json(response);
        }
    }
}
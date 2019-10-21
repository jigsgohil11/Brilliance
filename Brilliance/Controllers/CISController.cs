using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class CISController : Controller
    {
        private IConductIS _icisDataProvider;
        // GET: CIS
        public ActionResult Index()
        {
            TCFForm _TCFForm = new TCFForm();
            _TCFForm.TCFNotes=new List<TCFNotes>();
            _TCFForm.TCFTask = new List<TCFTask>();
            return View(_TCFForm);
        }
        public ActionResult OutComeMaster()
        {


            return View();
        }
        public JsonResult SaveOutcome(TCFQuestionGroup _TCFQuestionGroup)
        {
            _icisDataProvider = new ConductDataprovider();
            var response = new ServiceResponse();
            if (_TCFQuestionGroup!=null)
            {
                try
                {
                    _TCFQuestionGroup.IsDeleted =false;
                    _TCFQuestionGroup.SortingWeight = 0;
                    if (_TCFQuestionGroup.IsVisible == false)
                    {
                        _TCFQuestionGroup.IsVisible = true;
                    }
                    else if (_TCFQuestionGroup.IsVisible == true)
                    {
                        _TCFQuestionGroup.IsVisible = false;

                    }
                    if (_TCFQuestionGroup.coderef!=null)
                    {
                        _TCFQuestionGroup.Code = _TCFQuestionGroup.coderef;

                    }
                    if (_TCFQuestionGroup.IsEdit==false)
                    {
                       
                        _TCFQuestionGroup.Createddate = DateTime.Now;
                        _TCFQuestionGroup.CreatedBy = SessionHelper.UserId;

                    }
                    else if (_TCFQuestionGroup.IsEdit == true)
                    {
                        _TCFQuestionGroup.Modifieddate = DateTime.Now;
                        _TCFQuestionGroup.ModifiedBy = SessionHelper.UserId;

                    }
                    response = _icisDataProvider.SaveOutcome(_TCFQuestionGroup);
                   
                }
                catch(Exception ex)
                {
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);

                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getgroupcount()
        {
            _icisDataProvider = new ConductDataprovider();
            int res = 0;
            res = _icisDataProvider.GroupCount();
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult TCFOutcomeList()
        {
            _icisDataProvider = new ConductDataprovider();
            ConductDataViewModel _ConductDataViewModel = _icisDataProvider.TCFOutcomeList();
            return Json(_ConductDataViewModel.TCFQuestionGrouplist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetoutcomeById(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            var response = new TCFQuestionGroup();
            response = _icisDataProvider.GetoutcomeById(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        #region SubQuestions

        public ActionResult SuboutComes()
        {
            TCFQuestion _TCFQuestion = new TCFQuestion();
            return View(_TCFQuestion);
        }

        public JsonResult Bindgroup(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            var response = new List<SelectListItem>();
            response = _icisDataProvider.Bindgroup(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavesubOutcome(TCFQuestion _TCFQuestion)
        {
            _icisDataProvider = new ConductDataprovider();
            var response = new ServiceResponse();
            _TCFQuestion.IsDeleted = false;
            if (_TCFQuestion.IsEdit == false)
            {
               
                _TCFQuestion.CreatedAt = DateTime.Now;
                _TCFQuestion.CreatedBy = SessionHelper.UserId;

            }
            else if (_TCFQuestion.IsEdit == true)
            {
                _TCFQuestion.Modifieddate = DateTime.Now;
                _TCFQuestion.ModifiedBy = SessionHelper.UserId;

            }
            response = _icisDataProvider.SaveSuboutcomes(_TCFQuestion);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult QuestionList()
        {
            _icisDataProvider = new ConductDataprovider();
           List<TCFQuestionViewmodel> _TCFQuestionViewmodel = _icisDataProvider.TCFGetQuestionList();
            return Json(_TCFQuestionViewmodel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTCFQuestionById(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            var response = new TCFQuestion();
            response = _icisDataProvider.GetTCFQuestionById(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSuboutcomeCode(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            string res = string.Empty;
            Guid outcomId = new Guid(Id);
            res = _icisDataProvider.GetSuboutcomeCode(outcomId);
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        #endregion SuboutCome
        public JsonResult CisQuestion()
        {
            _icisDataProvider = new ConductDataprovider();
            ConductDataViewModel _ConductDataViewModel = _icisDataProvider.TCFQuestionGroupList();
            return Json(_ConductDataViewModel.TCFQuestionGrouplist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteoutcomeMaster(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
           var response = _icisDataProvider.DeleteoutcomeMaster(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);

        }
      
    }
}
using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class CISController : Controller
    {
        private IConductIS _icisDataProvider;
        // GET: CIS
        [HttpGet]
        public ActionResult Index()
        {
            TCFForm _TCFForm = new TCFForm();
         
            return View(_TCFForm);
        }
        [HttpPost]
        public ActionResult Index( TCFForm _TCFForm, List<HttpPostedFileBase> file)
        {
            var response = new ServiceResponse();
            _icisDataProvider = new ConductDataprovider();
           

            _TCFForm.CreatedAt = DateTime.Now;
            _TCFForm.CreatedBy = SessionHelper.UserId;
            _TCFForm.Description = _TCFForm.Description;
            if (_TCFForm.TCFOutCome.IsEdit==false)
            {
                TCFOutCome outcome = new TCFOutCome();
                
                outcome.AddedBy = SessionHelper.UserId;
                outcome.AddedDate = DateTime.Now;
                outcome.TCFQuestionGroupId = _TCFForm.TCFOutCome.TCFQuestionGroupId;
                _TCFForm.TCFOutCome = outcome;
                
                response = _icisDataProvider.SaveTCFForm(_TCFForm);
            }
            //if (file!=null)
            //{
            //    foreach (var _file in file)
            //    {
            //        TCFFormEvidence _TCFFormEvidence = new TCFFormEvidence();
            //        _TCFFormEvidence.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //        _TCFFormEvidence.FileName = Path.GetFileName(_file.FileName);

            //        var path = Path.Combine(Server.MapPath("~/Images/"), _file.FileName);
            //        _TCFFormEvidence.FileExtension = Path.GetExtension(path);
            //        _TCFFormEvidence.CreatedAt = DateTime.Now;
            //        _TCFFormEvidence.ExpiresAt = DateTime.Now.AddDays(10);
            //        _TCFFormEvidence.CreatedBy = SessionHelper.UserId;
            //        _TCFFormEvidence.IsDeleted = false;
            //      var  _response = _icisDataProvider.SaveTcfImage(_TCFFormEvidence);


            //    }

            //}
            
           
          
            return Json(response);

        }
        public ActionResult OutComeMaster()
        {


            return View();
        }

        public JsonResult MasterCont( string EntityId)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid entityId = new Guid(EntityId);
            if(EntityId!=null)
            {
                int response = _icisDataProvider.MasterCont(entityId);
                string _response = _icisDataProvider.Prcoutcometitle(entityId);
                return Json(new { response, _response }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);

            }

        }
        //public JsonResult Prcoutcometitle(string EntityId)
        //{
        //    _icisDataProvider = new ConductDataprovider();
        //    Guid entityId = new Guid(EntityId);

        //    string response = _icisDataProvider.Prcoutcometitle(entityId);
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GrupcntByEntity(int Id, string EntityId)
        {
            _icisDataProvider = new ConductDataprovider();
           
            Guid entityId = new Guid(EntityId);
           
            int response = _icisDataProvider.GrupcntByEntity(Id, entityId);
            string cde = _icisDataProvider.GetGroupSubCode(response);
            return Json(cde, JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteoutcomeMaster(string Id)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            var response = _icisDataProvider.DeleteoutcomeMaster(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);

        }
        #endregion SuboutCome

        #region TCFOperations
        public JsonResult CisQuestion()
        {
            _icisDataProvider = new ConductDataprovider();
            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            ConductDataViewModel _ConductDataViewModel = _icisDataProvider.TCFQuestionGroupList(userid);
            return Json(_ConductDataViewModel.TCFQuestionGrouplist, JsonRequestBehavior.AllowGet);
        }


        public JsonResult _GetSuboutcomeById(string Id)
        {
            TCFSubOutCome vm = new TCFSubOutCome();
            List<TCFFormEvidence> _Ev = new List<TCFFormEvidence>();
            List<TCFNotes> _Notes = new List<TCFNotes>();
            List<TCFTask> _task = new List<TCFTask>();
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            vm = _icisDataProvider._GetSuboutcomeById(outcomId);
           
            _Ev = _icisDataProvider._TCFFormEvidence(outcomId,vm.TCFQuestionId);
            _Notes= _icisDataProvider._GetNotes(outcomId, vm.TCFQuestionId);
            _task = _icisDataProvider._GetTask(outcomId, vm.TCFQuestionId);

            return Json(new { vm, _Ev }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult UpdateTCF(TCFForm _TCFForm, List<HttpPostedFileBase> file)
        {
            var response = new ServiceResponse();
            _icisDataProvider = new ConductDataprovider();


           
            if (file != null)
            {
                TCFFormEvidence _tcf = new TCFFormEvidence();
                _tcf.TcformId = _TCFForm.Id;
                _tcf.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                var res = _icisDataProvider.RemoveTcfImages(_tcf);
                foreach (var _file in file)
                {
                    TCFFormEvidence _TCFFormEvidence = new TCFFormEvidence();
                    _TCFFormEvidence.TcformId = _TCFForm.Id;

                    _TCFFormEvidence.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                    _TCFFormEvidence.FileName = Path.GetFileName(_file.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/"), _file.FileName);
                    _TCFFormEvidence.FileExtension = Path.GetExtension(path);
                    _TCFFormEvidence.CreatedAt = DateTime.Now;
                    _TCFFormEvidence.ExpiresAt = DateTime.Now.AddDays(10);
                    _TCFFormEvidence.CreatedBy = SessionHelper.UserId;
                    _TCFFormEvidence.IsDeleted = false;
                   
                    var _response = _icisDataProvider.SaveTcfImage(_TCFFormEvidence);


                }

            }
            if (_TCFForm.TCFOutCome.TCFSubOutCome.IsEdit == true)
            {
                TCFSubOutCome _sub = new TCFSubOutCome();
                _sub.Id = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
                _sub.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                _sub.RateId = _TCFForm.TCFOutCome.TCFSubOutCome.RateId;
                _sub.TCFSubOutComeUserId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFSubOutComeUserId;
                _sub.ReasonNotYet = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonNotYet;
                _sub.Reasonmostly = _TCFForm.TCFOutCome.TCFSubOutCome.Reasonmostly;
                _sub.ReasonNotApplicable = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonNotApplicable;
                _sub.ReasonPartially = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonPartially;
                _sub.ReasonFully = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonFully;
                _sub.ModifiedBy = SessionHelper.UserId;
                var _response = _icisDataProvider.UpdateUserSuboutcome(_sub);
            }
            if(_TCFForm.TCFOutCome.TCFSubOutCome.TCFTask.Count>0)
            {
                TCFTask _ts = new TCFTask();
                _ts.TcformId = _TCFForm.Id;
                _ts.TCFQuestionId= _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                _icisDataProvider.RemoveTcfTask(_ts);
                foreach (var item in _TCFForm.TCFOutCome.TCFSubOutCome.TCFTask.ToList())
                {
                    TCFTask _TCFTask = new TCFTask();
                    _TCFTask.TcformId = _TCFForm.Id;
                    _TCFTask.TCFSubOutComeId = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
                    _TCFTask.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                    _TCFTask.AddedById = SessionHelper.UserId;
                    _TCFTask.AssignUserId = item.AssignUserId;
                    _TCFTask.Task = item.Task;
                    _TCFTask.Status = item.Status;
                    var _response = _icisDataProvider.SaveTCFTask(_TCFTask);

                }


            }
            if (_TCFForm.TCFOutCome.TCFSubOutCome.TCFNotes.Count > 0)
            {
                TCFNotes _tns = new TCFNotes();
                _tns.TcformId = _TCFForm.Id;
                _tns.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                _icisDataProvider.RemoveTcfNotes(_tns);
                foreach (var item in _TCFForm.TCFOutCome.TCFSubOutCome.TCFNotes.ToList())
                {
                    TCFNotes _TCFNotes = new TCFNotes();
                    _TCFNotes.TcformId = _TCFForm.Id;
                    _TCFNotes.TCFSubOutComeId = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
                    _TCFNotes.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
                    _TCFNotes.AddedById = SessionHelper.UserId;
                    _TCFNotes.Author = item.Author;
                    _TCFNotes.Note = item.Note;
                    var _response = _icisDataProvider.SaveTCFNotes(_TCFNotes);

                }


            }
            return Json(response);

        }
        #endregion  TCFOperations
    }
}
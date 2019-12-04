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
        private ITemplateMaster _itemplateDataProvider;
        // GET: CIS
        [HttpGet]
        public ActionResult Index()
        {
            _icisDataProvider = new ConductDataprovider();
            Guid _user = SessionHelper.UserId;
            string Client = _icisDataProvider._GetEntityByUser(_user);
            TCFQuestion question = new TCFQuestion();
            question.TCFTask = new List<TCFTask>();
            question.TCFNotes = new List<TCFNotes>();

            return View(question);
        }
        [HttpPost]
        public ActionResult Index(TCFForm _TCFForm, List<HttpPostedFileBase> file)
        {
            var response = new ServiceResponse();
            _icisDataProvider = new ConductDataprovider();


            _TCFForm.CreatedAt = DateTime.Now;
            _TCFForm.CreatedBy = SessionHelper.UserId;
            _TCFForm.Description = _TCFForm.Description;
            if (_TCFForm.TCFOutCome.IsEdit == false)
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

        public JsonResult MasterCont(string EntityId)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid entityId = new Guid(EntityId);
            if (EntityId != null)
            {
                int response = _icisDataProvider.MasterCont(entityId);
                string _response = _icisDataProvider.Prcoutcometitle(entityId);
                List<SelectListItem> lst = new List<SelectListItem>();
                lst = _icisDataProvider.BindDivionByCompany(entityId);
                return Json(new { response, _response, lst }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveOutcome(TCFQuestionGroup _TCFQuestionGroup, HttpPostedFileBase file)
        {
            _icisDataProvider = new ConductDataprovider();
            var response = new ServiceResponse();
            string _FileName = string.Empty;
            if (_TCFQuestionGroup.coderef != null)
            {
                _TCFQuestionGroup.IsVisible = true;
            }
            else
            {
                _TCFQuestionGroup.IsVisible = false;

            }
            if (_TCFQuestionGroup != null)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                        file.SaveAs(_path);
                    }
                    _TCFQuestionGroup.IsDeleted = false;
                    _TCFQuestionGroup.SortingWeight = 0;
                    if (_TCFQuestionGroup.IsVisible == false)
                    {
                        _TCFQuestionGroup.IsVisible = true;
                    }
                    else if (_TCFQuestionGroup.IsVisible == true)
                    {
                        _TCFQuestionGroup.IsVisible = false;

                    }
                    if (_TCFQuestionGroup.coderef != null)
                    {
                        _TCFQuestionGroup.Code = _TCFQuestionGroup.coderef;

                    }
                    if (_TCFQuestionGroup.IsEdit == false)
                    {

                        _TCFQuestionGroup.Createddate = DateTime.Now;
                        _TCFQuestionGroup.CreatedBy = SessionHelper.UserId;
                        _TCFQuestionGroup.Filename = _FileName;
                    }
                    else if (_TCFQuestionGroup.IsEdit == true)
                    {
                        _TCFQuestionGroup.Modifieddate = DateTime.Now;
                        _TCFQuestionGroup.ModifiedBy = SessionHelper.UserId;
                        if (file != null && file.ContentLength > 0)
                        {
                            _TCFQuestionGroup.Filename = _FileName;

                        }
                        else
                        {
                            _TCFQuestionGroup.Filename = _TCFQuestionGroup.Filename;
                        }

                    }
                    response = _icisDataProvider.SaveOutcome(_TCFQuestionGroup);

                }
                catch (Exception ex)
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

            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            List<TCFQuestionViewmodel> _TCFQuestionViewmodel = _icisDataProvider.TCFGetQuestionList();
            return Json(_TCFQuestionViewmodel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TCFQuestionList()
        {
            _icisDataProvider = new ConductDataprovider();

            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            List<TCFQuestionViewmodel> _TCFQuestionViewmodel = _icisDataProvider._TCFQuestionForUser(userid);
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


        public JsonResult CisQuestion()
        {
            _icisDataProvider = new ConductDataprovider();
            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            ConductDataViewModel _ConductDataViewModel = _icisDataProvider.TCFQuestionGroupList(userid);
            return Json(_ConductDataViewModel.TCFQuestionGrouplist, JsonRequestBehavior.AllowGet);
        }



        public JsonResult TCFQuestionEntityUserWise()
        {
            _icisDataProvider = new ConductDataprovider();
            ConductDataViewModel _ConductDataViewModel = new ConductDataViewModel();
            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            Guid entity = new Guid();
            entity = SessionHelper.ClientID;
            string Position = _icisDataProvider._GetUserDivision(userid);
            if (Position != "")
            {
                Guid _Position = new Guid(Position);

                _ConductDataViewModel = _icisDataProvider.TCFQuestionEntityUserWise(entity, userid, 0, _Position);
            }
            else
            {
                Guid _Position = Guid.NewGuid();
                _ConductDataViewModel = _icisDataProvider.TCFQuestionEntityUserWise(entity, userid, 1, _Position);
            }

            return Json(_ConductDataViewModel.TCFQuestionGrouplist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _GetSuboutcomeById(string Id)
        {
            TCFQuestion vm = new TCFQuestion();
            List<TCFFormEvidence> _Ev = new List<TCFFormEvidence>();
            List<TCFNotes> _Notes = new List<TCFNotes>();
            List<TCFTask> _task = new List<TCFTask>();
            _icisDataProvider = new ConductDataprovider();
            Guid outcomId = new Guid(Id);
            vm = _icisDataProvider._GetSuboutcomeById(outcomId);
            if (vm != null)
            {
                _Ev = _icisDataProvider._TCFFormEvidence(vm.Id);
                _Notes = _icisDataProvider._GetNotes(vm.Id);
                _task = _icisDataProvider._GetTask(vm.Id);
                vm.TCFTask = _task;
            }

            return Json(new { vm, _Ev, _Notes }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetView(string Id, string viewName)
        {
            _icisDataProvider = new ConductDataprovider();
            TCFQuestion vm = new TCFQuestion();
            List<TCFTask> _task = new List<TCFTask>();
            Guid TaskId = new Guid(Id);
            ///// vm.Id = TaskId;
            _task = _icisDataProvider._GetTask(TaskId);
            if (_task != null)
            {
                foreach (var item in _task.ToList())
                {
                    TCFTask _t = new TCFTask();
                    _t._TaskStatus = _Status();
                    _t.Id = item.Id;
                    _t.Status = item.Status;
                    _t.Task = item.Task;
                    _t.AssignUserId = item.AssignUserId;
                    _t.DueDate = item.DueDate;
                    _t.TCFQuestionId = item.TCFQuestionId;
                    _t.AddedById = item.AddedById;
                    vm.TCFTask.Add(_t);
                }
            }
            /// vm.TCFTask = _task;

            return PartialView(viewName, vm);
        }
        public ActionResult GetNoteView(string Id, string viewName)
        {
            _icisDataProvider = new ConductDataprovider();
            TCFQuestion vm = new TCFQuestion();
            List<TCFNotes> _notes = new List<TCFNotes>();
            Guid noteId = new Guid(Id);
            ///// vm.Id = TaskId;
            _notes = _icisDataProvider._GetNotes(noteId);
            vm.TCFNotes = _notes;
            return PartialView(viewName, vm);
        }
        public JsonResult GetDivision(string CompanyId)
        {
            _icisDataProvider = new ConductDataprovider();
            Guid cid = new Guid(CompanyId);
            List<SelectListItem> lst = new List<SelectListItem>();
            lst = _icisDataProvider.BindDivionByCompany(cid);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateTCF(TCFQuestion _question, List<HttpPostedFileBase> file)
        {
            var response = new ServiceResponse();
            _icisDataProvider = new ConductDataprovider();
            if (_question.IsEdit == true)
            {
                response = _icisDataProvider._UpdateTcfFormEntryByUser(_question);
                if (file != null)
                {
                    TCFFormEvidence _tcf = new TCFFormEvidence();
                    _tcf.CreatedBy = SessionHelper.UserId;
                    _tcf.TCFQuestionId = _question.Id;
                    var res = _icisDataProvider.RemoveTcfImages(_tcf);
                    foreach (var _file in file)
                    {
                        TCFFormEvidence _TCFFormEvidence = new TCFFormEvidence();
                        _TCFFormEvidence.TCFQuestionId = _question.Id;
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
                if (_question.TCFNotes.Count > 0)
                {

                    TCFNotes _tcf = new TCFNotes();
                    _tcf.AddedById = SessionHelper.UserId;
                    _tcf.TCFQuestionId = _question.Id;
                    string note = _question.TCFNotes.Where(x => x.Note != null).Select(x => x.NoteId).FirstOrDefault();
                    if (note != null)
                    {
                        var res = _icisDataProvider.RemoveTcfNotes(_tcf);

                    }

                    string username = _icisDataProvider._GetUserName(SessionHelper.UserId);
                    foreach (var item in _question.TCFNotes.Where(x => x.Note != null).ToList())
                    {
                        TCFNotes _notes = new TCFNotes();
                        _notes.AddedById = SessionHelper.UserId;
                        _notes.TCFQuestionId = _question.Id;
                        _notes.Note = item.Note;
                        _notes.NoteId = item.NoteId;
                        _notes.Author = username.ToString();
                        response = _icisDataProvider.SaveTCFNotes(_notes);

                    }
                }
                if (_question.TCFTask.Count > 0)
                {
                    TCFTask _tcfTCFTask = new TCFTask();
                    _tcfTCFTask.AddedById = SessionHelper.UserId;
                    _tcfTCFTask.TCFQuestionId = _question.Id;
                    var res = _icisDataProvider.RemoveTcfTask(_tcfTCFTask);
                    foreach (var _item in _question.TCFTask.Where(x => x.Task != null).ToList())
                    {
                        TCFTask _tsk = new TCFTask();
                        _tsk.AddedById = SessionHelper.UserId;
                        _tsk.TCFQuestionId = _question.Id;
                        _tsk.Task = _item.Task;
                        _tsk.Status = _item.Status;
                        _tsk.DueDate = _item.DueDate;
                        _tsk.AssignUserId = _item.AssignUserId;

                        response = _icisDataProvider.SaveTCFTask(_tsk);

                    }
                }
            }
            else if (_question.IsEdit == false)
            {
                _question.Id = Guid.NewGuid();
                _question.RateId = _question.RateId;
                _question.TCFQuestionGroupId = _question.TCFQuestionGroupId;
                _question.BinderHolderTypeId = SessionHelper.ClientID;
                _question.Code = _question.Code;
                _question.Description = _question.Description;
                _question.GuidanceDescription = _question.Description;
                _question.IsStandard = false;
                _question.CreatedAt = DateTime.Now;
                _question.CreatedBy = SessionHelper.UserId;
                _question.IsDeleted = false;
                _question.ReasonNotYet = _question.ReasonNotYet;
                _question.Reasonmostly = _question.Reasonmostly;
                _question.ReasonNotApplicable = _question.ReasonNotApplicable;
                _question.ReasonPartially = _question.ReasonPartially;
                _question.ReasonFully = _question.ReasonFully;
                _question.TCFSubOutComeUserId = _question.TCFSubOutComeUserId;
                _question.DueDate = _question.DueDate;
                response = _icisDataProvider._NewTcfEntry(_question);
                if (file != null)
                {

                    foreach (var _file in file)
                    {
                        TCFFormEvidence _TCFFormEvidence = new TCFFormEvidence();
                        _TCFFormEvidence.TCFQuestionId = _question.Id;
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
                if (_question.TCFNotes.Count > 0)
                {
                    string username = _icisDataProvider._GetUserName(SessionHelper.UserId);
                    foreach (var item in _question.TCFNotes.ToList())
                    {
                        TCFNotes _notes = new TCFNotes();
                        _notes.AddedById = SessionHelper.UserId;
                        _notes.TCFQuestionId = _question.Id;
                        _notes.Note = item.Note;
                        _notes.Author = username.ToString();
                        _notes.NoteId = item.NoteId;
                        response = _icisDataProvider.SaveTCFNotes(_notes);

                    }
                }
                if (_question.TCFTask.Count > 0)
                {
                    foreach (var _item in _question.TCFTask.ToList())
                    {
                        TCFTask _tsk = new TCFTask();
                        _tsk.AddedById = SessionHelper.UserId;
                        _tsk.TCFQuestionId = _question.Id;
                        _tsk.Task = _item.Task;
                        _tsk.Status = _item.Status;
                        _tsk.DueDate = _item.DueDate;
                        _tsk.AssignUserId = _item.AssignUserId;

                        response = _icisDataProvider.SaveTCFTask(_tsk);

                    }
                }
            }
            //if (file != null)
            //{
            //    TCFFormEvidence _tcf = new TCFFormEvidence();
            //    _tcf.TcformId = _TCFForm.Id;
            //    _tcf.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //    var res = _icisDataProvider.RemoveTcfImages(_tcf);
            //    foreach (var _file in file)
            //    {
            //        TCFFormEvidence _TCFFormEvidence = new TCFFormEvidence();
            //        _TCFFormEvidence.TcformId = _TCFForm.Id;

            //        _TCFFormEvidence.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //        _TCFFormEvidence.FileName = Path.GetFileName(_file.FileName);

            //        var path = Path.Combine(Server.MapPath("~/Images/"), _file.FileName);
            //        _TCFFormEvidence.FileExtension = Path.GetExtension(path);
            //        _TCFFormEvidence.CreatedAt = DateTime.Now;
            //        _TCFFormEvidence.ExpiresAt = DateTime.Now.AddDays(10);
            //        _TCFFormEvidence.CreatedBy = SessionHelper.UserId;
            //        _TCFFormEvidence.IsDeleted = false;

            //        var _response = _icisDataProvider.SaveTcfImage(_TCFFormEvidence);


            //    }

            //}
            //if (_TCFForm.TCFOutCome.TCFSubOutCome.IsEdit == true)
            //{
            //    TCFSubOutCome _sub = new TCFSubOutCome();
            //    _sub.Id = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
            //    _sub.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //    _sub.RateId = _TCFForm.TCFOutCome.TCFSubOutCome.RateId;
            //    _sub.TCFSubOutComeUserId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFSubOutComeUserId;
            //    _sub.ReasonNotYet = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonNotYet;
            //    _sub.Reasonmostly = _TCFForm.TCFOutCome.TCFSubOutCome.Reasonmostly;
            //    _sub.ReasonNotApplicable = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonNotApplicable;
            //    _sub.ReasonPartially = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonPartially;
            //    _sub.ReasonFully = _TCFForm.TCFOutCome.TCFSubOutCome.ReasonFully;
            //    _sub.ModifiedBy = SessionHelper.UserId;
            //    _sub.DueDate = _TCFForm.TCFOutCome.TCFSubOutCome.DueDate;
            //    var _response = _icisDataProvider.UpdateUserSuboutcome(_sub);
            //}
            //if(_TCFForm.TCFOutCome.TCFSubOutCome.TCFTask.Count>0)
            //{
            //    TCFTask _ts = new TCFTask();
            //    _ts.TcformId = _TCFForm.Id;
            //    _ts.TCFQuestionId= _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //    _icisDataProvider.RemoveTcfTask(_ts);
            //    foreach (var item in _TCFForm.TCFOutCome.TCFSubOutCome.TCFTask.ToList())
            //    {
            //        TCFTask _TCFTask = new TCFTask();
            //        _TCFTask.TcformId = _TCFForm.Id;
            //        _TCFTask.TCFSubOutComeId = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
            //        _TCFTask.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //        _TCFTask.AddedById = SessionHelper.UserId;
            //        _TCFTask.AssignUserId = item.AssignUserId;
            //        _TCFTask.Task = item.Task;
            //        _TCFTask.Status = item.Status;
            //        var _response = _icisDataProvider.SaveTCFTask(_TCFTask);

            //    }


            //}
            //if (_TCFForm.TCFOutCome.TCFSubOutCome.TCFNotes.Count > 0)
            //{
            //    TCFNotes _tns = new TCFNotes();
            //    _tns.TcformId = _TCFForm.Id;
            //    _tns.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //    _icisDataProvider.RemoveTcfNotes(_tns);
            //    foreach (var item in _TCFForm.TCFOutCome.TCFSubOutCome.TCFNotes.ToList())
            //    {
            //        TCFNotes _TCFNotes = new TCFNotes();
            //        _TCFNotes.TcformId = _TCFForm.Id;
            //        _TCFNotes.TCFSubOutComeId = _TCFForm.TCFOutCome.TCFSubOutCome.Id;
            //        _TCFNotes.TCFQuestionId = _TCFForm.TCFOutCome.TCFSubOutCome.TCFQuestionId;
            //        _TCFNotes.AddedById = SessionHelper.UserId;
            //        _TCFNotes.Author = item.Author;
            //        _TCFNotes.Note = item.Note;
            //        var _response = _icisDataProvider.SaveTCFNotes(_TCFNotes);

            //    }


            //}
            return Json(response);

        }

        public List<TaskStatus> _Status()
        {
            List<TaskStatus> lst = new List<TaskStatus>();
            lst.Add(new TaskStatus { Id = "Assigned", Value = "Assigned" });
            lst.Add(new TaskStatus { Id = "In progress", Value = "In progress" });
            lst.Add(new TaskStatus { Id = "Completed", Value = "Completed" });
            return lst;
        }

        public ActionResult TemplateMaster()
        {


            return View();
        }
        public JsonResult SaveTemplateMaster(TemplateMaster template)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            var response = new ServiceResponse();
            string _FileName = string.Empty;
            try
            {
                if (template.IsEdit == false)
                {

                    template.CreatedByID = SessionHelper.UserId;
                    template.Datecreated = DateTime.Now;
                    response = _itemplateDataProvider.SaveTemplateMaster(template);

                }
                else if (template.IsEdit == true)
                {
                    template.ModifybyId = SessionHelper.UserId;
                    template.DateModfied = DateTime.Now;
                    response = _itemplateDataProvider.SaveTemplateMaster(template);

                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }



            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult TemplateList()
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            TemplateViewModel Viewmodel = _itemplateDataProvider.TemplateMasterList();
            return Json(Viewmodel.TemplateMaster, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateById(string Id)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid tid = new Guid(Id);
            TemplateMaster Viewmodel = _itemplateDataProvider.GetTemplateById(tid);
            return Json(Viewmodel, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TemplateOutcomemaster()
        {


            return View();
        }

        public JsonResult MasterTemplate(string TemplateId)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            Guid entityId = new Guid(TemplateId);
            if (TemplateId != null)
            {
                int response = _itemplateDataProvider.MasterCont(entityId);
                string _response = _itemplateDataProvider.Prcoutcometitle(entityId);
              
                return Json(new { response, _response }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public ActionResult TemplateOutcomemaster(TemplateOutcomemaster model)
        {
            var response = new ServiceResponse();
            _itemplateDataProvider = new TemplatemasterDataProvider();
            if(model.IsEdit==false)
            {
                model.Createddate = DateTime.Now;
                model.Createdby = SessionHelper.UserId;
                model.IsDeleted = false;
                if (model.coderef != null)
                {
                   
                    model.IsVisible = false;
                }
                else
                {
                    model.IsVisible = true;

                }
                response = _itemplateDataProvider.Addoutcomemastertemplate(model);
            }
            else
            {
                if (model.coderef != null)
                {
                    model.IsVisible = true;
                }
                else
                {
                    model.IsVisible = false;

                }
                model.Modifieddate = DateTime.Now;
                model.ModifiedBy = SessionHelper.UserId;
                model.Createddate = null;
                model.Createdby = null;
                model.IsDeleted = false;
                response = _itemplateDataProvider.Addoutcomemastertemplate(model);
            }

            return Json(response);
        }

        public JsonResult TemplatedOutcomeList()
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            TemplateViewModel _vm= _itemplateDataProvider.TemplateOutcomeList();
            return Json(_vm._TemplateOutcomemaster, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateOutcomeById(string Id)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid tid = new Guid(Id);
            TemplateOutcomemaster vm = _itemplateDataProvider.GetTemplateOutcomeById(tid);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Grouptemplatecount(int Id, string EntityId)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid entityId = new Guid(EntityId);

            int response = _itemplateDataProvider.GrupcntByEntity(Id, entityId);
            string cde = _itemplateDataProvider.GetGroupSubCode(response);
            return Json(cde, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TemplateSubOutcomemaster()
        {


            return View();
        }
        [HttpPost]
        public ActionResult TemplateSubOutcomemaster(TemplateSuboutcomeMaster _TemplateSuboutcomeMaster)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            var response = new ServiceResponse();
            if (_TemplateSuboutcomeMaster.IsEdit==false)
            {
                _TemplateSuboutcomeMaster.CreatedAt = DateTime.Now;
                _TemplateSuboutcomeMaster.CreatedBy = SessionHelper.UserId;
                response = _itemplateDataProvider.SavetemplateSuboutcomemaster(_TemplateSuboutcomeMaster);
            }
            else
            {
                _TemplateSuboutcomeMaster.Modifieddate = DateTime.Now;
                _TemplateSuboutcomeMaster.ModifiedBy = SessionHelper.UserId;
                response = _itemplateDataProvider.SavetemplateSuboutcomemaster(_TemplateSuboutcomeMaster);

            }

            return Json(response);
        }
        public JsonResult BindOutcomemastergroup(string Id)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid outcomId = new Guid(Id);
            var response = new List<SelectListItem>();
            response = _itemplateDataProvider.Bindgroup(outcomId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateSuboutcomeCode(string Id)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            string res = string.Empty;
            Guid outcomId = new Guid(Id);
            res = _itemplateDataProvider.GetSuboutcomeCode(outcomId);
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetTemplatesuboutcomelist()
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();
            TemplateViewModel _vm = _itemplateDataProvider.GetTemplatesuboutcomelist();
            return Json(_vm._TemplateSuboutcomeMaster, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplateSuboutcomeMasterById(string Id)
        {
            _itemplateDataProvider = new TemplatemasterDataProvider();

            Guid tid = new Guid(Id);
            TemplateSuboutcomeMaster vm = _itemplateDataProvider.GetTemplateSuboutcomeMasterById(tid);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AttachStandardTocompany()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AttachStandardTocompany(StandardConduct _StandardConduct)
        {

            _itemplateDataProvider = new TemplatemasterDataProvider();
            var response = new ServiceResponse();
            _StandardConduct.CreatedBy = SessionHelper.UserId;
            _StandardConduct.CreatedDate = DateTime.Now;
            response = _itemplateDataProvider.AttachStandard(_StandardConduct);


            return View(response);
        }
    }

}





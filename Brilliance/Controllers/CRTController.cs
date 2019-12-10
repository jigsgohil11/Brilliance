using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;
using System.Data;
using System.Web;
using System.IO;

namespace Brilliance.Controllers
{
    public class CRTController : BaseController
    {
        private IComplaintDataProvider _icomplaintDataProvider;
        // GET: CRT
        public ActionResult CRTList()
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            ComplaintListModel complaintlistModel = _icomplaintDataProvider.ComplaintList();
            return View(complaintlistModel);
        }
        public ActionResult CRT(string id)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();
            Guid ComplaintID = Common.CheckIdNullOrEmpty(id);
            response = _icomplaintDataProvider.GetComplaint(ComplaintID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveComplaint(ComplaintViewModel Complaintmodel, FormCollection fc)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();

            int rowCount = 0;
            if (Complaintmodel.NoteList != null && Complaintmodel.NoteList.Count > 0)
            {
                rowCount = Complaintmodel.NoteList.Count;
            }
            if (Complaintmodel != null && Complaintmodel.complaint != null && Complaintmodel.complaint.ComplaintID != null && Complaintmodel.complaint.ComplaintID != Guid.Empty)
            {
                Guid ComplaintId = Complaintmodel.complaint.ComplaintID;
                Complaintmodel.complaint.IsEdit = true;
            }
            else
            {
                Guid ComplaintId = Guid.NewGuid();
                Complaintmodel.complaint.ComplaintID = ComplaintId;
                Complaintmodel.complaint.IsEdit = false;
            }
            string strPath = Convert.ToString(Complaintmodel.complaint.ComplaintID);


            DataTable complaint_task = new DataTable();
            complaint_task.TableName = "mst_CRTNotes";
            complaint_task.Columns.Add("NoteID", typeof(Guid));
            complaint_task.Columns.Add("ComplaintID", typeof(Guid));
            complaint_task.Columns.Add("FileNoteID", typeof(string));
            complaint_task.Columns.Add("FileNoteDate", typeof(DateTime));
            complaint_task.Columns.Add("NoteText", typeof(string));
            complaint_task.Columns.Add("FilePath", typeof(string));
            complaint_task.Columns.Add("Author", typeof(string));
            complaint_task.Columns.Add("CreatedBy", typeof(Guid));
            complaint_task.Columns.Add("CreatedOn", typeof(DateTime));


            DataRow dr = null;
            string fileid = "";
            if (rowCount > 0)
            {
                foreach (string fileName in Request.Files)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        var index = i + 1;

                        if (index < 10)
                        {
                            index = int.Parse("0" + index);
                            fileid = "0" + Convert.ToString(index);
                        }
                        if (fileName.ToString() == Convert.ToString(fc["NoteList[" + i + "].Attachment"]).Replace(",", ""))
                        {
                            if (Request.Files[fileName].FileName != "")
                            {
                                dr = complaint_task.NewRow();
                                dr["NoteID"] = Guid.NewGuid();
                                dr["ComplaintID"] = Complaintmodel.complaint.ComplaintID;
                                dr["FileNoteID"] = fileid;
                                dr["FileNoteDate"] = DateTime.Now;
                                if (!string.IsNullOrEmpty(fc["NoteList[" + i + "].NoteText"]))
                                    dr["NoteText"] = fc["NoteList[" + i + "].NoteText"];
                                dr["FilePath"] = UploadComplaintDocuments(Request.Files[fileName], strPath, Complaintmodel.complaint.ComplaintID);

                                dr["Author"] = SessionHelper.UserId;
                                dr["CreatedBy"] = SessionHelper.UserId;
                                dr["CreatedOn"] = DateTime.Now;
                                complaint_task.Rows.Add(dr);
                            }
                            else
                            {
                                dr = complaint_task.NewRow();
                                dr["NoteID"] = Guid.NewGuid();
                                dr["ComplaintID"] = Complaintmodel.complaint.ComplaintID;
                                dr["FileNoteID"] = fileid;
                                dr["FileNoteDate"] = DateTime.Now;
                                if (!string.IsNullOrEmpty(fc["NoteList[" + i + "].NoteText"]))
                                    dr["NoteText"] = fc["NoteList[" + i + "].NoteText"];
                                dr["FilePath"] = null;
                                dr["Author"] = SessionHelper.UserId;
                                dr["CreatedBy"] = SessionHelper.UserId;
                                dr["CreatedOn"] = DateTime.Now;
                                complaint_task.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            response = _icomplaintDataProvider.SaveComplaints(Complaintmodel, complaint_task);
            if (response.IsSuccess)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Failed"] = response.Message;
            }
            //return Json(response);
            return RedirectToAction("CRTList", "CRT");

        }
        public static string UploadComplaintDocuments(HttpPostedFileBase File, string FilePath, Guid ComplaintID)
        {
            string strReturn = "";
            try
            {
                if (File != null)
                {

                    var FileNameToSave = ComplaintID + Path.GetExtension(File.FileName);

                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/"));

                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + FilePath)))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + FilePath));

                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + FilePath + "/"), File.FileName);

                    File.SaveAs(path);

                    strReturn = File.FileName;
                }
                return strReturn;
            }
            catch
            {
                return strReturn;
            }
        }
        public ActionResult DeleteComplaint(string DeleteID)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            var response = new ServiceResponse();
            Guid ComplaintID = Common.CheckIdNullOrEmpty(DeleteID);
            response = _icomplaintDataProvider.DeleteComplaint(ComplaintID);
            return Json(response);
        }

        public ActionResult GetCompanyCustomRules(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetCompanyCustomRules(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDivisionByCompany(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetDivisionByCompany(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductByCompany(string cid)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid CompanyID = Common.CheckIdNullOrEmptyNonEncrypt(cid);
            ServiceResponse response = _icomplaintDataProvider.GetProductByCompany(CompanyID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductByProductCategory(string id)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid ProductCategoryID = Common.CheckIdNullOrEmptyNonEncrypt(id);
            ServiceResponse response = _icomplaintDataProvider.GetProductByProductCategory(ProductCategoryID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNatureOfComplaintList(string id)
        {
            _icomplaintDataProvider = new ComplaintDataProvider();
            Guid ComplaintCategoryID = Common.CheckIdNullOrEmptyNonEncrypt(id);
            ServiceResponse response = _icomplaintDataProvider.GetNatureOfComplaintList(ComplaintCategoryID);
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }



    }
}
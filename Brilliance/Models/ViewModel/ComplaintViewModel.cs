using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class ComplaintViewModel
    {
        public ComplaintViewModel()
        {
            complaint = new Complaint();
            NoteList = new List<CRTNotes>();
            CompanyList = new List<SelectListItem>();
            DivisionList = new List<SelectListItem>();
            DisatisfactionLevelList = new List<SelectListItem>();
            ProductCategorylist = new List<SelectListItem>();
            ProductTypeList = new List<SelectListItem>();
            ComplaintCategoryList = new List<SelectListItem>();
            ComplaintNatureList = new List<SelectListItem>();
            PolicyList = new List<SelectListItem>();
            RootcauseList = new List<SelectListItem>();
            ComplaintreceivedList = new List<SelectListItem>();
            ComplaintReceivedRegulatoryList = new List<SelectListItem>();
            ComplaintReceivedRegulatoryFeedbackList = new List<SelectListItem>();
            SatisfactionLevelResolutionList = new List<SelectListItem>();
            ComplaintOverallOutcomeList = new List<SelectListItem>();
            ComplaintCompensationList = new List<SelectListItem>();
            ComplaintRegulatedCostList = new List<SelectListItem>();
            CaseFileStageList = new List<SelectListItem>();
            UserList = new List<SelectListItem>();
            Labelconfig = new LabelConfiguration();
            client = new Client();
        }
        public Complaint complaint { get; set; }
        public List<CRTNotes> NoteList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> DivisionList { get; set; }
        public List<SelectListItem> DisatisfactionLevelList { get; set; }
        public List<SelectListItem> ProductCategorylist { get; set; }
        public List<SelectListItem> ProductTypeList { get; set; }
        public List<SelectListItem> ComplaintCategoryList { get; set; }
        public List<SelectListItem> ComplaintNatureList { get; set; }
        public List<SelectListItem> PolicyList { get; set; }
        public List<SelectListItem> RootcauseList { get; set; }
        public List<SelectListItem> ComplaintreceivedList { get; set; }
        public List<SelectListItem> ComplaintReceivedRegulatoryList { get; set; }
        public List<SelectListItem> ComplaintReceivedRegulatoryFeedbackList { get; set; }
        public List<SelectListItem> SatisfactionLevelResolutionList { get; set; }
        public List<SelectListItem> ComplaintOverallOutcomeList { get; set; }
        public List<SelectListItem> ComplaintCompensationList { get; set; }
        public List<SelectListItem> ComplaintRegulatedCostList { get; set; }
        public List<SelectListItem> CaseFileStageList { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public LabelConfiguration Labelconfig { get; set; }
        public Client client { get; set; }
    }

    public class ComplaintListModel
    {
        public ComplaintListModel()
        {
            Complaintlist = new List<Complaint>();
            Response = new ServiceResponse();
        }
        public List<Complaint> Complaintlist { get; set; }
        public ServiceResponse Response { get; set; }
    }

    public class ComplaintReportModel
    {
        public ComplaintReportModel()
        {
            Reportlist = new List<SelectListItem>();
            report = new ReportType();
            //Response = new ServiceResponse();
        }
        public List<SelectListItem> Reportlist { get; set; }
        public ReportType report { get; set; }
        //public ServiceResponse Response { get; set; }
    }

    public class ReportType
    {

        [Required(ErrorMessage = "Report Type is required.")]
        public Guid ReportTypeID { get; set; }
        public string ReportTypeName { get; set; }
        public string RoleName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }

}
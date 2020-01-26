using Brilliance.Infrastructure;
using Brilliance.Models.Entity;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Brilliance.Models.ViewModel
{
    public class CrtAdminViewmodel
    {
        public CrtAdminViewmodel()
        {
            client = new Client();
            clientList = new List<SelectListItem>();
            labelconfig = new LabelConfiguration();
            labelconfiglist = new List<LabelConfiguration>();
        }
        public Client client { get; set; }
        public List<SelectListItem> clientList { get; set; }
        public LabelConfiguration labelconfig { get; set; }
        public List<LabelConfiguration> labelconfiglist { get; set; }
    }

    public class DropSelectViewmodel
    {
        public DropSelectViewmodel()
        {
            productcategoryList = new List<SelectListItem>();
            producttypeList = new List<SelectListItem>();
            complaintcategoryList = new List<SelectListItem>();
            natureList = new List<SelectListItem>();
            TcfList = new List<SelectListItem>();
            policystatusList = new List<SelectListItem>();
            rootList = new List<SelectListItem>();
            complaintreceivedList = new List<SelectListItem>();
            complaintregulatoryList = new List<SelectListItem>();
            feedbackregulatoryList = new List<SelectListItem>();
            overalloutcomeList = new List<SelectListItem>();
            compensationList = new List<SelectListItem>();
            regulatedcostList = new List<SelectListItem>();
            dropselect = new dropselectconfig();
        }

        public List<SelectListItem> productcategoryList { get; set; }
        public List<SelectListItem> producttypeList { get; set; }
        public List<SelectListItem> complaintcategoryList { get; set; }
        public List<SelectListItem> natureList { get; set; }
        public List<SelectListItem> TcfList { get; set; }
        public List<SelectListItem> policystatusList { get; set; }
        public List<SelectListItem> rootList { get; set; }
        public List<SelectListItem> complaintreceivedList { get; set; }
        public List<SelectListItem> complaintregulatoryList { get; set; }
        public List<SelectListItem> feedbackregulatoryList { get; set; }
        public List<SelectListItem> overalloutcomeList { get; set; }
        public List<SelectListItem> compensationList { get; set; }
        public List<SelectListItem> regulatedcostList { get; set; }
        public dropselectconfig dropselect { get; set; }

    }

    public class DropselectModel
    {
        public DropselectModel()
        {
            dropselectconfig = new dropselectconfig();
            ProductcategoryList = new List<SelectListItem>();
            ComplaintcategoryList = new List<SelectListItem>();
            NatureList = new List<SelectListItem>();
            projecttermlist = new List<projectterm>();
        }
        public dropselectconfig dropselectconfig { get; set; }
        public List<SelectListItem> ProductcategoryList { get; set; }
        public List<SelectListItem> ComplaintcategoryList { get; set; }
        public List<SelectListItem> NatureList { get; set; }
        public List<projectterm> projecttermlist { get; set; }

    }

    public class dropselectconfig
    {
        public Guid ProjectTermID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid? RefTermID { get; set; }
        public Guid? RefTermID1 { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? ProjecttermCategoryID { get; set; }
        public string ProjecttermCategoryName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class projectterm
    {
        public Guid TermID { get; set; }
        public string Term { get; set; }
        public string AboutTerm { get; set; }
        public Guid? RefTermID { get; set; }
        public Guid? RefTermID1 { get; set; }
        public Guid? ClientID { get; set; }
        public string Refname { get; set; }
        public string Refname1 { get; set; }

    }



    //public class CrtAdminLabelConfigViewmodel
    //{
    //    public CrtAdminLabelConfigViewmodel()
    //    {
    //        labelconfig = new LabelConfiguration();
    //    }
    //    public LabelConfiguration labelconfig { get; set; }

    //}
}
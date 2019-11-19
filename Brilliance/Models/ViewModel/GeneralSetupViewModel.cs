using Brilliance.Infrastructure;
using Brilliance.Models;
using PetaPoco;
using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class GeneralSetupViewModel
    {
        public GeneralSetupViewModel()
        {
            TermCategory = new ProjectTermCategory();
            CategoryList = new List<ProjectTermList>();
            Response = new ServiceResponse();
        }
        public ProjectTermCategory TermCategory { get; set; }
        public List<ProjectTermList> CategoryList { get; set; }
        public ServiceResponse Response { get; set; }
    }
    public class ProjectTermList
    {
        public Guid ProjectTermCategoryID { get; set; }
        public string ProjectTermCategoryName { get; set; }
        public string DisplayName { get; set; }
        [Ignore]
        public string EncryptedProjectTermCategoryID { get { return ProjectTermCategoryID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ProjectTermCategoryID)) : null; } }
        public bool IsSelectedCategory { get; set; }
    }

    public class ProjectTermCategoryList
    {

        public Guid ProjectTermID { get; set; }
        public Guid ProjectTermCategoryID { get; set; }
        public string ProjectTermCategoryName { get; set; }
        public string TermCode { get; set; }
        public string DefaultValue { get; set; }
        public string StartDate { get { return DefaultValue != null ? Common.ConvertDateFormate(Convert.ToDateTime(DefaultValue)) : ""; } }

        public string EndDate { get { return DefaultValue1 != null ? Common.ConvertDateFormate(Convert.ToDateTime(DefaultValue1)) : ""; } }
        public string DefaultValue1 { get; set; }
        public string DefaultValue2 { get; set; }
        public string DisplayName { get; set; }
        public string Term { get; set; }
        public string CurrencyRate { get { return DefaultValue != null ? DefaultValue : ""; } }
        public string SectorType { get; set; }
        [Ignore]
        public string EncryptedProjectTermID { get { return ProjectTermID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ProjectTermID)) : null; } }
    }

    public class ProjectTermListByCategory
    {
        public ProjectTermListByCategory()
        {
            projecttermcategory = new ProjectTermCategory();
            CategoryList = new List<ProjectTermList>();
            Projecttermcategorylist = new List<ProjectTermCategoryList>();
            ComplaintCategoryList = new List<SelectListItem>();
            IndustryList = new List<SelectListItem>();
            OrganizationList = new List<SelectListItem>();
            Response = new ServiceResponse();
        }
        public ProjectTermCategory projecttermcategory { get; set; }
        public List<ProjectTermList> CategoryList { get; set; }
        public List<ProjectTermCategoryList> Projecttermcategorylist { get; set; }
        public List<SelectListItem> ComplaintCategoryList { get; set; }
        public List<SelectListItem> IndustryList { get; set; }
        public List<SelectListItem> OrganizationList { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
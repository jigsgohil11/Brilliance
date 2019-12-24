using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class AdmDivisionViewModel
    {
        public AdmDivisionViewModel()
        {
            //client = new Client();
            division = new Division();
            Companylist = new List<SelectListItem>();
            TypeList = new List<SelectListItem>();
            divisionList = new List<divisionforUDT>();
        }
        //public Client client { get; set; }
        public Division division { get; set; }
        public List<SelectListItem> Companylist { get; set; }
        public List<SelectListItem> TypeList { get; set; }
        public List<divisionforUDT> divisionList { get; set; }
    }

    public class divisionforUDT {
        
        public Guid DivisionID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? ClientID { get; set; }
        public Guid DivisionType { get; set; }
        public bool IsEdit { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }
    }

}
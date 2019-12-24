using Brilliance.Infrastructure;
using Brilliance.Models.Entity;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class AdmCompanyViewModel
    {
        public AdmCompanyViewModel()
        {
            company = new Company();
            //Companylist = new List<SelectListItem>();
            //TypeList = new List<SelectListItem>();
            companyList = new List<companyforUDT>();
        }
        public Company company { get; set; }
        //public List<SelectListItem> Companylist { get; set; }
        //public List<SelectListItem> TypeList { get; set; }
        public List<companyforUDT> companyList { get; set; }
    }

    public class companyforUDT
    {

        public Guid CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public Guid? ClientID { get; set; }
        public string FspNo { get; set; }
        public int divisioncount { get; set; }
        public bool IsEdit { get; set; }
        [Ignore]
        public string EncryptedCompanyID { get { return CompanyID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(CompanyID)) : null; } }
        [Ignore]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }
    }
}
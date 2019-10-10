using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            company = new Company();
            ClientList = new List<SelectListItem>();
            IndustryList = new List<SelectListItem>();
            SectorList = new List<SelectListItem>();
            Countrylist = new List<SelectListItem>();
            StateList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
        }
        public Company company { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<SelectListItem> IndustryList { get; set; }
        public List<SelectListItem> SectorList { get; set; }
        public List<SelectListItem> Countrylist { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }

    }
    public class CompanyListModel
    {
        public CompanyListModel()
        {
            Companylist = new List<Company>();
            Response = new ServiceResponse();
        }
        public List<Company> Companylist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
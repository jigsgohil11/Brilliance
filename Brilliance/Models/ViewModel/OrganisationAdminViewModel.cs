using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class OrganisationAdminViewModel
    {
        public OrganisationAdminViewModel()
        {
            client = new Client();
            //Countrylist = new List<SelectListItem>();
            //StateList = new List<SelectListItem>();
            //CityList = new List<SelectListItem>();
        }
        public Client client { get; set; }
        //public List<SelectListItem> Countrylist { get; set; }
        //public List<SelectListItem> StateList { get; set; }
        //public List<SelectListItem> CityList { get; set; }
    }

    public class Allcount
    {
        public int countorganisation { get; set; }
        public int countcompany { get; set; }
        public int countdivision { get; set; }
        public int countCRT { get; set; }
        public int countCIS { get; set; }
    }


    public class OrganisationListModel
    {
        public OrganisationListModel()
        {
            Organisationlist = new List<Client>();
            allcount = new Allcount();
            Response = new ServiceResponse();

        }
        public List<Client> Organisationlist { get; set; }
        public Allcount allcount { get; set; }
        public ServiceResponse Response { get; set; }
    }

    public class CompanyAdminModel
    {
        public CompanyAdminModel()
        {
            company = new Company();
            Companylist = new List<Company>();
        }
        public Company company { get; set; }
        public List<Company> Companylist { get; set; }

    }

}
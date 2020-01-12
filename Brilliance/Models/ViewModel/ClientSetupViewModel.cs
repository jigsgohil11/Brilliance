using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class ClientSetupViewModel
    {
        public ClientSetupViewModel()
        {
            client = new Client();
            Countrylist = new List<SelectListItem>();
            //StateList = new List<SelectListItem>();
            //CityList = new List<SelectListItem>();
        }
        public Client client { get; set; }
        public List<SelectListItem> Countrylist { get; set; }
        //public List<SelectListItem> StateList { get; set; }
        //public List<SelectListItem> CityList { get; set; }
    }
}
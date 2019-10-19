using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class DivisionViewModel
    {
        public DivisionViewModel()
        {
            division = new Division();
            Clientlist = new List<SelectListItem>();
            CompanyList = new List<SelectListItem>();
        }
        public Division division { get; set; }
        public List<SelectListItem> Clientlist { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
    }

    public class DivisionListModel
    {
        public DivisionListModel()
        {
            Divisionlist = new List<Division>();
            Response = new ServiceResponse();
        }
        public List<Division> Divisionlist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
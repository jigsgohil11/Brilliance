using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            product = new Product();
            Clientlist = new List<SelectListItem>();
            Companylist = new List<SelectListItem>();
            ProductTypelist = new List<SelectListItem>();
        }
        public Product product { get; set; }
        public List<SelectListItem> Clientlist { get; set; }
        public List<SelectListItem> Companylist { get; set; }
        public List<SelectListItem> ProductTypelist { get; set; }
    }
    public class ProductListModel
    {
        public ProductListModel()
        {
            Productlist = new List<Product>();
            Response = new ServiceResponse();
        }
        public List<Product> Productlist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
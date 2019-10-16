using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class ProductController : Controller
    {
        private IProductDataProvider _iproductDataProvider;

        public ActionResult OrganizationList()
        {
            _iproductDataProvider = new ProductDataProvider();
            ProductListModel productlistModel = _iproductDataProvider.ProductList();
            return View(productlistModel);
        }
        public ActionResult Organization(string id)
        {
            _iproductDataProvider = new ProductDataProvider();
            var response = new ServiceResponse();
            Guid ProductID = Common.CheckIdNullOrEmpty(id);
            response = _iproductDataProvider.GetProduct(ProductID);
            return View(response.Data);

        }
        [HttpPost]
        public ActionResult SaveProduct(ProductViewModel productViewModel)
        {
            _iproductDataProvider = new ProductDataProvider();
            var response = new ServiceResponse();
            response = _iproductDataProvider.SaveProducts(productViewModel);
            return Json(response);
        }
        public ActionResult DeleteProduct(string DeleteID)
        {
            _iproductDataProvider = new ProductDataProvider();
            var response = new ServiceResponse();
            Guid ProductId = Common.CheckIdNullOrEmpty(DeleteID);
            response = _iproductDataProvider.DeleteProduct(ProductId);
            return Json(response);
        }       
    }
}
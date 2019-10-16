using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IProductDataProvider
    {
        ProductListModel ProductList();
        ServiceResponse GetProduct(Guid ProductID);
        ServiceResponse SaveProducts(ProductViewModel Products);
        ServiceResponse DeleteProduct(Guid ProductID);
        //ServiceResponse GetStateByCountry(Guid CountryID);
        //ServiceResponse GetCityByState(Guid StateID);

    }
}
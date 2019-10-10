using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface ICompanyDataProvider
    {
        CompanyListModel CompanyList();
        ServiceResponse GetCompany(Guid CompanyID);
        ServiceResponse SaveCompanies(CompanyViewModel companymodel);
        ServiceResponse DeleteCompany(Guid CompanyID);
        //ServiceResponse GetStateByCountry(Guid CountryID);
        //ServiceResponse GetCityByState(Guid StateID);
    }
}

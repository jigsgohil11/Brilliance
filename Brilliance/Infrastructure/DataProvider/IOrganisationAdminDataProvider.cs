using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;


namespace Brilliance.Infrastructure.DataProvider
{
    interface IOrganisationAdminDataProvider
    {
        OrganisationListModel OrganisationList();
        ServiceResponse GetCompany(Guid ClientID);
        ServiceResponse SaveCompany(CompanyAdminModel companymodel);
        //ServiceResponse DeleteClient(Guid ClientID);
        //ServiceResponse GetStateByCountry(Guid CountryID);
        //ServiceResponse GetCityByState(Guid StateID);

    }
}

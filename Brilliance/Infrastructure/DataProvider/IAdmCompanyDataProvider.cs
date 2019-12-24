using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IAdmCompanyDataProvider
    {
        ServiceResponse GetCompany(Guid ClientID);
        ServiceResponse SaveCompany(AdmCompanyViewModel companyModel);
        ServiceResponse DeleteCompany(Guid CompanyID);
    }
}

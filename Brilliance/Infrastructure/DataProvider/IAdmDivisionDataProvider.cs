using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IAdmDivisionDataProvider
    {
        ServiceResponse GetDivision(Guid ClientID,Guid CompanyID);
        ServiceResponse SaveDivision(AdmDivisionViewModel divisionModel);
        ServiceResponse DeleteDivision(Guid DivisionID);
    }
}

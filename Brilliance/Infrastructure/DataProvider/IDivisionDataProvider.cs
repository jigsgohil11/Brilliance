using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;



namespace Brilliance.Infrastructure.DataProvider
{
    interface IDivisionDataProvider
    {
        DivisionListModel DivisionList();
        ServiceResponse GetDivision(Guid DivisionID);
        ServiceResponse SaveDivision(DivisionViewModel Divisionmodel);
        ServiceResponse DeleteDivision(Guid DivisionID);
        ServiceResponse GetCompanyByClient(Guid ClientID);
    }
}

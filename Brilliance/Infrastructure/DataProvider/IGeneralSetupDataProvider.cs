using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IGeneralSetupDataProvider
    {
        ProjectTermListByCategory TermlistbyCategory();
        ProjectTermListByCategory CategoryList(Guid ProjectTermCategoryID);
        ProjectTermListByCategory GeneralsetupPopup(Guid ProjectTermID);
        ServiceResponse SaveGeneralSetup1(ProjectTermListByCategory ProjectCategoryModel);
        ServiceResponse DeleteSetup(Guid ProjectTermID);
        ProjectTermListByCategory AddGeneralsetupPopup(Guid Categoryid, string CategoryName);
    }
}

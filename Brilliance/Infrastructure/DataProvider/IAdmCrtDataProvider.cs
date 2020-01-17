using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Web;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IAdmCrtDataProvider
    {
        ServiceResponse GetCrtSetup();
        ServiceResponse Savelabelconfig(CrtAdminViewmodel CRTAdminVM);
        CrtAdminViewmodel labelconfiglist(Guid ClientID);
        CrtAdminViewmodel EditLabelConfig(Guid ClientID);
    }
}

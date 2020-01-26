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
        DropSelectViewmodel DropselectgList(Guid ClientID);
        DropselectModel AddDropSelect(string Category, Guid ClientID);
        ServiceResponse Savedropselectconfig(Guid TermId, Guid ClientID, Guid? Refid, Guid? Refid1, string name, string desc, string category, bool isedit);
        ServiceResponse Deletedropselectconfig(Guid TermId);

    }
}



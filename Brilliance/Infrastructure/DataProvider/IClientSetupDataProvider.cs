using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Web;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IClientSetupDataProvider
    {
        ServiceResponse GetClient(Guid ClientID);
        ServiceResponse SaveClient(ClientSetupViewModel Clients,HttpPostedFileBase orglogo);
        ServiceResponse GetInstanceDetail(Guid ClientID);
        ServiceResponse Getsubscription(Guid ClientID);
        ServiceResponse SaveInstance(AppSubscriptionViewModel AppModel);
    }
}

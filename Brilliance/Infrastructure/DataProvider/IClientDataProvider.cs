using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IClientDataProvider
    {
        ClientListModel ClientList();
        ServiceResponse GetClient(Guid ClientID);
        ServiceResponse SaveClients(ClientViewModel Clients);
        ServiceResponse DeleteClient(Guid ClientID);
        ServiceResponse GetStateByCountry(Guid CountryID);
        ServiceResponse GetCityByState(Guid StateID);


    }
}
                                                                                                                                                                                               
using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IUserDataProvider
    {
        UsersListModel UserList();
        ServiceResponse GetUser(Guid UserID);
        ServiceResponse SaveUser(UserViewModel Usermodel);
        ServiceResponse DeleteUser(Guid UserID);
        //ServiceResponse GetStateByCountry(Guid CountryID);
        //ServiceResponse GetCityByState(Guid StateID);
        RoleListModel RoleList();
        ServiceResponse GetRole(Guid RoleID);
        ServiceResponse SaveRole(RoleViewModel Rolemodel);
        ServiceResponse DeleteRole(Guid RoleID);
    }
}
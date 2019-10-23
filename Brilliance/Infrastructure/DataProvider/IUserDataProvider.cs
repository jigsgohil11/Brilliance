using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IUserDataProvider
    {
        UserListModel UserList();
        ServiceResponse GetUser(Guid UserID);
        ServiceResponse SaveUser(UserViewModel Usermodel);
        ServiceResponse DeleteUser(Guid UserID);
        RoleListModel RoleList();
        ServiceResponse GetRole(Guid RoleID);
        ServiceResponse SaveRole(RoleViewModel Rolemodel);
        ServiceResponse DeleteRole(Guid RoleID);
    }
}
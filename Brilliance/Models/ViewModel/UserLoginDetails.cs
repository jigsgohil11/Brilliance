using Brilliance.Models.Entity;
using System.Collections.Generic;

namespace Brilliance.Models.ViewModel
{
    public class UserLoginDetails
    {
        public UserLoginDetails()
        {
            LoginUserData = new LoginUser();
            loginUserRoleRightModel = new List<LoginUserRoleRightModel>();
        }
        public LoginUser LoginUserData { get; set; }
        public List<LoginUserRoleRightModel> loginUserRoleRightModel { get; set; }
    }
}
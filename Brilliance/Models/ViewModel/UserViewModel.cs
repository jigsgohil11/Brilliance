using Brilliance.Models.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            User = new User();
            RoleList = new List<SelectListItem>();
        }
        public User User { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
    public class UserListModel
    {
        public UserListModel()
        {
            Userlist = new List<User>();
            Response = new ServiceResponse();
        }
        public List<User> Userlist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
using Brilliance.Models.Entity;
using System.Collections.Generic;

namespace Brilliance.Models.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            User = new User();           
        }
        public User User { get; set; }
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
using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliance.Models.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new Users();           
        }
        public Users Users { get; set; }
    }    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliance.Models.ViewModel
{
    public class LoginUserRoleRightModel
    {
        public Guid PrevillageID { get; set; }
        public string DisplayName { get; set; }
        public bool IsView { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsCreate { get; set; }
    }
}
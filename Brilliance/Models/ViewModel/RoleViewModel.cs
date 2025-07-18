﻿using Brilliance.Models.Entity;
using System.Collections.Generic;

namespace Brilliance.Models.ViewModel
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            role = new Role();
            //loginUserRoleRightModel = new List<LoginUserRoleRightModel>();
        }
        public Role role { get; set; }
        //public List<LoginUserRoleRightModel> loginUserRoleRightModel { get; set; }

    }
    public class RoleListModel
    {
        public RoleListModel()
        {
            Rolelist = new List<Role>();
            Response = new ServiceResponse();
        }
        public List<Role> Rolelist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}
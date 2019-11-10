using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class UsersModel
    {
        public UsersModel()
        {
            Users = new Users();
            RoleList = new List<SelectListItem>();
            RegionList = new List<SelectListItem>();
            ConstituencyList = new List<SelectListItem>();
            LocationList = new List<SelectListItem>();
            usergroupList = new List<SelectListItem>();
            programlist = new List<SelectListItem>();
            //UserRegion = new User_UserRegion();
        }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> RegionList { get; set; }
        public List<SelectListItem> ConstituencyList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> usergroupList { get; set; }
        public List<SelectListItem> programlist { get; set; }

        public Users Users { get; set; }
        //public User_UserRegion UserRegion { get; set; }
    }

    public class UsersListModel
    {
        public UsersListModel()
        {
            Users = new List<Users>();
            Response = new ServiceResponse();
        }
        public List<Users> Users { get; set; }
        public ServiceResponse Response { get; set; }
    }


    public class UsersLoginDetails
    {
        public UsersLoginDetails()
        {
            LoginUserDataModel = new List<LoginUserDataModel>();
            LoginUserRoleRightModel = new List<LoginUserRoleRightModel>();
        }
        public List<LoginUserDataModel> LoginUserDataModel { get; set; }
        public List<LoginUserRoleRightModel> LoginUserRoleRightModel { get; set; }
    }

    public class LoginUserDataModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid ClientID { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string PhotoPath { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public string RegionIDS { get; set; }
        public string ConstituencyIDs { get; set; }
        public string LocationIDS { get; set; }
        public string ProgrammeID { get; set; }
    }

    //public class LoginUserRoleRightModel
    //{
    //    public Guid RightID { get; set; }
    //    public string RightName { get; set; }
    //    public bool IsView { get; set; }
    //    public bool IsUpdate { get; set; }
    //    public bool IsDelete { get; set; }
    //    public bool IsCreate { get; set; }
    //}
}
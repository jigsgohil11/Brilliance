using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Brilliance.Infrastructure.DataProvider
{
    public class UserDataProvider : BaseDataProvider, IUserDataProvider
    {
        public ServiceResponse GetUser(Guid UserID)
        {
            UserViewModel UserViewModel = new UserViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "UserID" ,Value = Convert.ToString(UserID)}
                };

                UserViewModel = GetMultipleEntity<UserViewModel>("GetUserdata", searchList);
                response.Data = UserViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public UserListModel UserList()
        {
            var userListModel = new UserListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<User> userlist = GetEntityList<User>("GetUserList", searchList);//Constants.GetUserGroupList
                if (userlist.Count > 0)
                {
                    userListModel.Userlist = userlist;
                    userListModel.Response.IsSuccess = true;
                }
                else
                {
                    userListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return userListModel;
        }
        public ServiceResponse SaveUser(UserViewModel Usermodel)
        {
            var response = new ServiceResponse();
            try
            {


                //if (Usermodel.Users.IsEdit == false)
                //{

                //    SqlCommand cmd = new SqlCommand();
                //    //cmd.Parameters.AddWithValue("@UserID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@Code", Usermodel.User.Code).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@Name", Usermodel.User.Name).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@Description", Usermodel.User.Description).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@CompanyID", Usermodel.User.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@ClientID", Usermodel.User.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                //    //cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                //    //cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;


                //    DataSet ds = null;
                //    ds = BulkInsert("Save_User", cmd);

                //    response.IsSuccess = true;

                //    response.Message = "Record Saved Successfully.";
                //}
                //else
                //{
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.AddWithValue("@UserID", Usermodel.Users.UserID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@Code", Usermodel.Users.Code).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Name", Usermodel.Users.Name).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Description", Usermodel.Users.Description).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@CompanyID", Usermodel.Users.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@ClientID", Usermodel.Users.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                //cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;

                DataSet ds = null;
                ds = BulkInsert("Save_User", cmd);
                response.IsSuccess = true;
                response.Message = "Record Updated Successfully.";
                //}

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
        public ServiceResponse DeleteUser(Guid UserID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (UserID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "UserID", Value = Convert.ToString(UserID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteUser", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public ServiceResponse GetRole(Guid RoleID)
        {
            RoleViewModel RoleViewModel = new RoleViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "RoleID" ,Value = Convert.ToString(RoleID)}
                };

                RoleViewModel = GetMultipleEntity<RoleViewModel>("GetRoledata", searchList);
                response.Data = RoleViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public RoleListModel RoleList()
        {
            var roleListModel = new RoleListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Role> roles = GetEntityList<Role>("GetRoleList", searchList);//Constants.GetUserGroupList
                if (roles.Count > 0)
                {
                    roleListModel.Rolelist = roles;
                    roleListModel.Response.IsSuccess = true;
                }
                else
                {
                    roleListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return roleListModel;
        }
        public ServiceResponse SaveRole(RoleViewModel Rolemodel)
        {
            var response = new ServiceResponse();
            try
            {


                //if (Usermodel.Users.IsEdit == false)
                //{

                //    SqlCommand cmd = new SqlCommand();
                //    //cmd.Parameters.AddWithValue("@UserID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@Code", Usermodel.User.Code).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@Name", Usermodel.User.Name).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@Description", Usermodel.User.Description).SqlDbType = SqlDbType.NVarChar;
                //    //cmd.Parameters.AddWithValue("@CompanyID", Usermodel.User.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@ClientID", Usermodel.User.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                //    //cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                //    //cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                //    //cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;


                //    DataSet ds = null;
                //    ds = BulkInsert("Save_User", cmd);

                //    response.IsSuccess = true;

                //    response.Message = "Record Saved Successfully.";
                //}
                //else
                //{
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.AddWithValue("@UserID", Usermodel.Users.UserID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@Code", Usermodel.Users.Code).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Name", Usermodel.Users.Name).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Description", Usermodel.Users.Description).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@CompanyID", Usermodel.Users.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@ClientID", Usermodel.Users.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                //cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;

                DataSet ds = null;
                ds = BulkInsert("Save_Role", cmd);
                response.IsSuccess = true;
                response.Message = "Record Updated Successfully.";
                //}

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
        public ServiceResponse DeleteRole(Guid RoleID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (RoleID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "RoleID", Value = Convert.ToString(RoleID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteRole", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
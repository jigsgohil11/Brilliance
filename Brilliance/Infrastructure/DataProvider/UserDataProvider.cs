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
                //var pwd= Crypto.Decrypt(UserViewModel.User.Password);
                //UserViewModel.User.Password = pwd;
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
            DataSet ds = null;
            var response = new ServiceResponse();
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (Usermodel.User.IsEdit == false)
                {
                    cmd.Parameters.AddWithValue("@UserID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@FirstName", Usermodel.User.FirstName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@LastName", Usermodel.User.LastName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@EmailID", Usermodel.User.EmailID).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Password", Usermodel.User.Password.Trim()).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", Usermodel.User.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@PhoneNumber", Usermodel.User.PhoneNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RoleID", Usermodel.User.SelectedRoleID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;

                    ds = BulkInsert("Save_User", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@UserID", Usermodel.User.UserID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@FirstName", Usermodel.User.FirstName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@LastName", Usermodel.User.LastName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@EmailID", Usermodel.User.EmailID).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Password", Usermodel.User.Password.Trim()).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", Usermodel.User.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@PhoneNumber", Usermodel.User.PhoneNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RoleID", Usermodel.User.SelectedRoleID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Usermodel.User.IsEdit).SqlDbType = SqlDbType.Bit;

                    ds = BulkInsert("Save_User", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Updated Successfully.";
                }
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
                DataSet ds = null;
                SqlCommand cmd = new SqlCommand();
                if (Rolemodel.role.IsEdit == false)
                {
                    cmd.Parameters.AddWithValue("@RoleID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RoleCode", Rolemodel.role.RoleCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RoleName", Rolemodel.role.RoleName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@AboutRole", Rolemodel.role.AboutRole).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", Rolemodel.role.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsDefault", true).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@OrderNo", null).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Rolemodel.role.IsEdit).SqlDbType = SqlDbType.Bit;

                    ds = BulkInsert("Save_Role", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RoleID", Rolemodel.role.RoleID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RoleCode", Rolemodel.role.RoleCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RoleName", Rolemodel.role.RoleName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@AboutRole", Rolemodel.role.AboutRole).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", Rolemodel.role.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsDefault", true).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@OrderNo", null).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Rolemodel.role.IsEdit).SqlDbType = SqlDbType.Bit;

                    ds = BulkInsert("Save_Role", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Updated Successfully.";
                }
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
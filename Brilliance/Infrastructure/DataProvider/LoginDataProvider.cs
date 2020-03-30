using System;
using System.Collections.Generic;
using System.Web;
using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System.Linq;

namespace Brilliance.Infrastructure.DataProvider
{
    public class LoginDataProvider : BaseDataProvider, ILoginDataProvider
    {
        public ServiceResponse Login(string UserName, string Password)
        {
            ServiceResponse response = new ServiceResponse();
            UserLoginDetails logindetails = new UserLoginDetails();
            try
            {
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    var searchlist = new List<SearchValueData>();
                    var searchValueData = new SearchValueData { Name = "UserName", Value = UserName.Trim() };
                    searchlist.Add(searchValueData);

                    searchValueData = new SearchValueData { Name = "Password", Value = Password.Trim() };
                    searchlist.Add(searchValueData);
                    bool IsValidUser = false;

                    logindetails = GetMultipleEntity<UserLoginDetails>("Brilliance_GetLoginData", searchlist);
                    if (logindetails.LoginUserData != null)
                    {
                        if (logindetails.LoginUserData.UserID != null)
                        {
                            //SessionHelper.ClientID = logindetails.LoginUserData.ClientID;
                            SessionHelper.UserId = logindetails.LoginUserData.UserID;
                            SessionHelper.UserName = logindetails.LoginUserData.UserName;
                            SessionHelper.Password = logindetails.LoginUserData.Password;
                            SessionHelper.FullName = logindetails.LoginUserData.FullName;
                            //SessionHelper.EmployeeID = logindetails.LoginUserData.EmployeeID;
                            //SessionHelper.Issu = logindetails.LoginUserData.IsSuperAdmin;
                            //SessionHelper.UserName = logindetails.LoginUserData.DisplayName;
                            //SessionHelper.EmailID = logindetails.LoginUserData.SecondaryEmailID;
                            //SessionHelper.RoleName = logindetails.LoginUserData.RoleName.ToString().Trim();
                            SessionHelper.Permissions = string.Join(",", logindetails.loginUserRoleRightModel.Select(m => m.DisplayName.Trim().ToLower()).ToList());
                            SessionHelper.LoginUserRoleRightList = System.Web.Helpers.Json.Encode(logindetails.loginUserRoleRightModel);
                            //if (!string.IsNullOrEmpty(logindetails.LoginUserData.ProfileImage))
                            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(Constants.ProfileImagesPath.Replace("~", "") + logindetails.LoginUserData.ProfileImage)))
                            //        SessionHelper.Photo = Constants.ProfileImagesPath.Replace("~", "") + logindetails.LoginUserData.ProfileImage;
                        }

                        IsValidUser = true;
                        LoginLog loginLog = new LoginLog();
                        //loginLog.LoginDateTime = DateTime.Now;
                        loginLog.SessionID = HttpContext.Current.Session.SessionID;
                        loginLog.UserID = SessionHelper.UserId;

                        SaveEntity<LoginLog>(loginLog);
                        SessionHelper.LoginLogID = loginLog.LoginLogID;

                    }

                    if (IsValidUser)
                    {
                        response.IsSuccess = true;
                        response.Message = "Welcome " + SessionHelper.UserName;
                        //response.Data = SessionHelper.DefaultLandingPageURL;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "UserName or Password not matched.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }
            return response;
        }
        public ServiceResponse ResetPassword(ChangePassword changePasswordModel)
        {

            var response = new ServiceResponse();
            try
            {
                Guid userId = Common.CheckIdNullOrEmpty(changePasswordModel.EncryptedUserID);
                if (userId != Guid.Empty)
                {
                    Users users = GetEntity<Users>(userId);

                    changePasswordModel.NewPassword = Crypto.Encrypt(changePasswordModel.NewPassword);
                    users.Password = changePasswordModel.NewPassword;
                    users.IsVerified = true;
                    users.IsActive = true;

                    SaveEntity(users);

                    response.Message = "Password Set Successfully";
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Oops Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Internal server error";
            }
            return response;
        }

        public ServiceResponse ResetPassword(Guid userId)
        {
            var response = new ServiceResponse();
            ChangePassword changePasswordModel = new ChangePassword();
            try
            {
                if (userId != Guid.Empty)
                {
                    Users user = GetEntity<Users>(userId);
                    if (user != null)
                    {
                        changePasswordModel.EncryptedUserID = user.EncryptedUserID;
                        changePasswordModel.IsActive = user.IsActive;
                        changePasswordModel.IsVerified = user.IsVerified;
                        response.IsSuccess = true;
                        response.Data = changePasswordModel;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Oops Something Went Wrong";
                    }
                    //BaseDataProvider obj = new BaseDataProvider();
                    //user.IsActive = true;
                    //obj.SaveEntity(user);                    
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Oops Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = true;
            }
            return response;
        }

        public ServiceResponse ForgotPasswordDetails(string EmailID)
        {
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                if (!string.IsNullOrEmpty(EmailID))
                {
                    var searchValueData = new SearchValueData { Name = "EmailID", Value = EmailID };
                    searchList.Add(searchValueData);

                    Users user = GetEntity<Users>("HrOms_User_GetUserByEmailID", searchList);

                    if (user != null)
                    {
                        //if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Areas/Setup/EmailTemplates/ForgetPasswordDetails.html")))
                        //{
                        try
                        {
                            //var emailTemplateLM = new CommonEmailTemplateListModel();
                            //var searchList1 = new List<SearchValueData>()
                            //    {
                            //        new SearchValueData { Name = "TemplateName" ,Value = "Forgot Password"}
                            //    };
                            //List<CommonEmailTemplate> emailTemplateList = GetEntityList<CommonEmailTemplate>("hra_EmailTemplateListData", searchList1);

                            //string strHTML = emailTemplateList[0].EmailBody;
                            //string Subject = emailTemplateList[0].EmailSubject;
                            //strHTML = strHTML.Replace("[User_Name]", user.UserName).Replace("[Reset_Password_Link]", ConfigSettings.BasePath + ConfigSettings.ResetPasswordLink + user.EncryptedUserID);
                            //response.IsSuccess = Common.SendEmail(user.SecondaryEmailID, Subject, strHTML);
                            response.Message = "Password Reset link sent to your Email Account";
                            //string strHTML = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Areas/Setup/EmailTemplates/ForgetPasswordDetails.html"));
                            //string Subject = "Your Credential Of Aneja Associates";
                            //strHTML = strHTML.Replace("##UserName##", user.UserName).Replace("##ResetPasswordLink##", ConfigSettings.BasePath + ConfigSettings.ResetPasswordLink + user.EncryptedUserID);
                            //response.IsSuccess = Common.SendEmail(user.SecondaryEmailID, Subject, strHTML);
                            //response.Message = "Password Reset link sent to your Email Account";
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Internal server error";
                        }
                        //}
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Email doesnot exists";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Enter valid email address";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Internal server error";
            }
            return response;
        }


    }
}
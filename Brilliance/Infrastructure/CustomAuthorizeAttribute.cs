using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.ViewModel;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using System;

namespace Brilliance.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        readonly string _loginURL = ConfigSettings.BasePath + "/" + Constants.LoginURL;
        readonly string _accessDeniedUrl = ConfigSettings.BasePath + "/" + Constants.AccessDeniedUrl;

        public string Permissions { get; set; }
        public string PermissionType { get; set; }
        //public string Module { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            string queryString = Convert.ToString(filterContext.RequestContext.RouteData.Values["id"]);
            bool isQueryString = !string.IsNullOrEmpty(queryString);

            if (CheckAllowedActions())
                return;

            if (SessionHelper.UserId != Guid.Empty)
            {
                string[] strPermissions = SessionHelper.Permissions.Split(',');
                bool isAuthorized = (strPermissions.Contains(Permissions.Trim().ToLower()));

                LoginUserRoleRightModel loginUserRoleRightModel = new LoginUserRoleRightModel(); //Common.IsPermissionAllowed(Permissions.Trim().ToLower());

                if (!isAuthorized)
                {
                    RedirectToAction(filterContext, _accessDeniedUrl);
                }
                if (PermissionType == Constants.Can_View && !loginUserRoleRightModel.IsView)
                {
                    RedirectToAction(filterContext, _accessDeniedUrl);
                }
                else if (PermissionType == Constants.Can_CreateOrUpdate)
                {
                    var CreateOrUpdate = PermissionType.Split(',');
                    string canCreate = CreateOrUpdate[0].ToString();
                    string canUpdate = CreateOrUpdate[1].ToString();

                    if (!loginUserRoleRightModel.IsCreate)
                        RedirectToAction(filterContext, _accessDeniedUrl);
                    if (isQueryString && !loginUserRoleRightModel.IsUpdate)
                        RedirectToAction(filterContext, _accessDeniedUrl);
                }
                else if (PermissionType == Constants.Can_Delete && !loginUserRoleRightModel.IsDelete)
                {
                    RedirectToAction(filterContext, _accessDeniedUrl);
                }

            }
            else
            {
                RedirectToAction(filterContext, _loginURL);
            }

        }

        private void RedirectToAction(AuthorizationContext filterContext, string actionURL)
        {
            var isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjaxRequest)
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new LinkResponse
                    {
                        Type = Constants.NotAuthorized,
                        Link = actionURL
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
                filterContext.Result = new RedirectResult(actionURL);
        }

        private bool CheckAllowedActions()
        {
            string[] strPermissions = string.IsNullOrEmpty(Permissions) ? new string[] { } : Permissions.Split(',');

            if (strPermissions.Contains(Constants.AnonymousPermission))
                return true;

            if (!strPermissions.Any())
                return true;

            return false;
        }
    }
}
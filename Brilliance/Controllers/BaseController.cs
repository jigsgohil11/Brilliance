using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using System.Web.Routing;
using Elmah;

namespace Brilliance.Controllers
{
    public class BaseController : Controller
    {
        public static bool IsOperationsSuspended = false;

        [HttpPost]
        public virtual JsonResult Autocomplete(string table, string textField, string valueField, string text, int pageSize, string SearchCriteriaField1 = "", string SearchCriteriaValue1 = "", string SearchCriteriaField2 = "", string SearchCriteriaValue2 = "")
        {
            BaseDataProvider baseDataProvider = new BaseDataProvider();
            return Json(baseDataProvider.GetAutoCompleteList(table, textField, valueField, text, pageSize, SearchCriteriaField1 != "" ? string.Format(" and {0} like '{1}'", SearchCriteriaField1, SearchCriteriaValue1) : "", SearchCriteriaField2 != "" ? string.Format(" and {0} like '{1}'", SearchCriteriaField2, SearchCriteriaValue2) : ""));
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool countRequest = false;

            if (SessionHelper.UserId == Guid.Empty && SessionHelper.ClientID == Guid.Empty)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "login",
                    action = "login"
                }));
                return;
            }

            //if (filterContext.HttpContext.Request.IsAjaxRequest() && SessionHelper.FirstTimeLogin)
            //{
            //    SessionHelper.FirstTimeLogin = false;
            //    countRequest = true;
            //}

            //if (!SessionHelper.FirstTimeLogin)
            //{
            //    if (!countRequest)
            //    {
            //        if (SessionHelper.UserId != Guid.Empty && SessionHelper.ClientID != Guid.Empty)
            //        {
            //            BaseDataProvider bdp = new BaseDataProvider();
            //            var searchList = new List<SearchValueData>();
            //            var searchValueData = new SearchValueData { Name = "UserID1", Value = SessionHelper.UserId.ToString() };
            //            searchList.Add(searchValueData);

            //            searchValueData = new SearchValueData { Name = "ClientID1", Value = SessionHelper.ClientID.ToString() };
            //            searchList.Add(searchValueData);

            //            searchValueData = new SearchValueData { Name = "IsRequest", Value = Convert.ToString(1) };
            //            searchList.Add(searchValueData);

            //            //List<LoginUserRoleRightModel> updatedUserRoleRightList = bdp.GetEntityList<LoginUserRoleRightModel>("user_User_Get_RoleRightDataByUser", searchList);

            //            UsersLoginDetails usersLoginDetails = bdp.GetMultipleEntity<UsersLoginDetails>("user_User_GetLoginDataNew", searchList);
            //            UserManagementDataProvider userManagementDataProvider = new UserManagementDataProvider();

            //            userManagementDataProvider.FillUserSession(usersLoginDetails);
            //            SessionHelper.FirstTimeLogin = false;
            //        }
            //    }
            //    countRequest = false;
            //}
            //base.OnActionExecuting(filterContext);
            //var request = HttpContext.Request;
            //var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath);
            //ViewBag.BasePath = baseUrl;
        }

        public object GetQueryStringValue(string encryptedQueryString, string queryname)
        {
            if (!string.IsNullOrEmpty(encryptedQueryString))
            {
                string invitation = Crypto.Decrypt(encryptedQueryString);
                if (!invitation.Contains("&"))
                {
                    if (invitation.StartsWith(queryname))
                    {
                        string[] values = invitation.Split('=');
                        return values[1];
                    }
                }
                else
                {
                    string[] invitations = invitation.Split('&');
                    return
                        (from inv in invitations where inv.StartsWith(queryname) select inv.Split('=')).Select(
                            values => values[1]).FirstOrDefault();
                }
            }
            return null;
        }

        public List<T> AddEncryptedURL<T>(List<T> items, string queryName) where T : class, new()
        {
            T itm = new T();
            PropertyInfo queryNameInfo = itm.GetType().GetProperty(queryName);
            PropertyInfo encryptedURLInfo = itm.GetType().GetProperty(Constants.EncryptedQueryString);
            if (queryNameInfo != null && encryptedURLInfo != null)
            {
                foreach (T item in items)
                {
                    string encryptedQueryString = Crypto.Encrypt(string.Format("{0}={1}", queryName, queryNameInfo.GetValue(item, null)));
                    encryptedURLInfo.SetValue(item, encryptedQueryString, null);
                }
            }
            return items;
        }

        public List<T> AddEncryptedURL<T>(List<T> items, string[] queryNames) where T : class, new()
        {
            T itm = new T();
            PropertyInfo encryptedURLInfo = itm.GetType().GetProperty(Constants.EncryptedQueryString);
            if (encryptedURLInfo != null)
            {
                foreach (T item in items)
                {
                    string encryptedQueryString = "";
                    foreach (string queryName in queryNames)
                    {
                        PropertyInfo queryNameInfo = itm.GetType().GetProperty(queryName);
                        if (queryNameInfo != null)
                            encryptedQueryString += string.Format("{0}={1}&", queryName, queryNameInfo.GetValue(item, null));
                    }
                    if (!string.IsNullOrEmpty(encryptedQueryString))
                        encryptedURLInfo.SetValue(item, Crypto.Encrypt(encryptedQueryString.TrimEnd('&')), null);
                }
            }

            return items;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                var httpContext = filterContext.HttpContext.ApplicationInstance.Context;
                var signal = ErrorSignal.FromContext(httpContext);
                signal.Raise(filterContext.Exception, httpContext);
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    filterContext.Result = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new
                        {
                            filterContext.Exception.Message
                        }
                    };
                }
                else
                {
                    var model = new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());

                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(model)
                    };
                }


                if (filterContext.Exception.GetType() == typeof(HttpException))
                {
                    HttpException exception = filterContext.Exception as HttpException;
                    filterContext.HttpContext.Response.StatusCode = exception.GetHttpCode();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected ContentResult CustJson<T>(T model)
        {
            return new JsonStringResult(Common.SerializeObject(model));
        }
    }
}
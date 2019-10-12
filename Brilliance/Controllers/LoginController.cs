using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Brilliance.Controllers
{
    public class LoginController : Controller
    {
        private ILoginDataProvider _loginDataProvider;
        // GET: Login
        public ActionResult Login()
        {
            if (SessionHelper.UserId == Guid.Empty)
            {
                if (Request.Cookies["BrillianceUserName"] != null && Request.Cookies["BrilliancePassword"] != null)
                {
                    if (Request.Cookies["BrillianceUserName"].Value != null && Request.Cookies["BrilliancePassword"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Cookies["BrillianceUserName"].Value) && !string.IsNullOrEmpty(Request.Cookies["BrilliancePassword"].Value))
                        {
                            var _loginDataProvider = new LoginDataProvider();
                            ServiceResponse response = _loginDataProvider.Login(Crypto.Decrypt(Request.Cookies["BrillianceUserName"].Value), Crypto.Decrypt(Request.Cookies["BrilliancePassword"].Value));
                            TempData["Message"] = response.Message;
                            if (response.IsSuccess)
                            {
                                //SessionHelper.CurrentModuleName = "Dashboard";
                                return RedirectToAction("Dashboard", "Dashboard");
                                //return RedirectToAction("CRT", "CRT");
                            }
                        }
                    }
                }
                return View(new Login());
            }
            else
            {
                return RedirectToAction("Dashboard", "Dashboard");
                //return RedirectToAction("CRT", "CRT");
            }
           
        }

        public ActionResult Logout()
        {
            if (SessionHelper.LoginLogID != Guid.Empty)
            {
                Signout();
            }
            return RedirectToAction("login", "login");
        }

       

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            UserLoginDetails logindata = new UserLoginDetails();
            ServiceResponse response = new ServiceResponse();
            try
            {
                if (fc != null && !string.IsNullOrEmpty(fc["UserName"]) && !string.IsNullOrEmpty(fc["Password"]))
                {
                    _loginDataProvider = new LoginDataProvider();
                    response = _loginDataProvider.Login(fc["UserName"].Trim(), fc["Password"].Trim());
                    if (response.IsSuccess)
                    {
                        //SessionHelper.CurrentModuleName = "Dashboard";
                        if (fc["RememberMe"] != null)
                        {
                            if (!string.IsNullOrEmpty(fc["RememberMe"]))
                            {
                                HttpCookie CookieName = new HttpCookie("BrillianceUserName");
                                CookieName.Value = Crypto.Encrypt(fc["UserName"].ToString());
                                CookieName.Expires = DateTime.Now.AddDays(10);
                                Response.Cookies.Add(CookieName);

                                HttpCookie cookiepassword = new HttpCookie("BrilliancePassword");
                                cookiepassword.Value = Crypto.Encrypt(fc["Password"].ToString());
                                cookiepassword.Expires = DateTime.Now.AddDays(10);
                                Response.Cookies.Add(cookiepassword);
                            }
                        }
                        return Json(response);
                    }
                    else
                    {
                        HttpCookie CookieName = Request.Cookies["BrillianceUserName"];
                        HttpCookie cookiepassword = Request.Cookies["BrilliancePassword"];

                        if (cookiepassword != null && CookieName != null)
                        {
                            CookieName.Expires = DateTime.Now.AddDays(-1);
                            CookieName.Value = null;
                            Response.Cookies.Add(CookieName);

                            cookiepassword.Expires = DateTime.Now.AddDays(-1);
                            cookiepassword.Value = null;
                            Response.Cookies.Add(cookiepassword);
                        }
                    }
                }
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
            }
            return Json(response);
        }

      
        

        public void Signout()
        {
            BaseDataProvider bdp = new BaseDataProvider();
            LoginLog loginLog = bdp.GetEntity<LoginLog>(SessionHelper.LoginLogID);
            loginLog.LogoutDateTime = DateTime.Now;
            bdp.SaveEntity<LoginLog>(loginLog);

            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie CookieName = Request.Cookies["BrillianceUserName"];
            HttpCookie cookiepassword = Request.Cookies["BrilliancePassword"];

            if (cookiepassword != null && CookieName != null)
            {
                CookieName.Expires = DateTime.Now.AddDays(-1);
                CookieName.Value = null;
                Response.Cookies.Add(CookieName);

                cookiepassword.Expires = DateTime.Now.AddDays(-1);
                cookiepassword.Value = null;
                Response.Cookies.Add(cookiepassword);
            }
        }
    }
}
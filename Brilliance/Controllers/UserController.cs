using Brilliance.Infrastructure;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;
using System.Web.Mvc;

namespace Brilliance.Controllers
{
    public class UserController : Controller
    {
        private IUserDataProvider _iuserDataProvider;
        // GET: User
        public ActionResult UserList()
        {
            _iuserDataProvider = new UserDataProvider();
            UsersListModel UserlistModel = _iuserDataProvider.UserList();
            return View(UserlistModel);
        }
        public ActionResult User(string id)
        {
            _iuserDataProvider = new UserDataProvider();
            var response = new ServiceResponse();
            Guid UserID = Common.CheckIdNullOrEmpty(id);
            response = _iuserDataProvider.GetUser(UserID);
            return View(response.Data);
        }
        [HttpPost]
        public ActionResult SaveUser(UserViewModel UserViewModel)
        {
            _iuserDataProvider = new UserDataProvider();
            var response = new ServiceResponse();
            response = _iuserDataProvider.SaveUser(UserViewModel);
            return Json(response);
        }
        public ActionResult DeleteUser(string DeleteID)
        {
            _iuserDataProvider = new UserDataProvider();
            var response = new ServiceResponse();
            Guid UserID = Common.CheckIdNullOrEmpty(DeleteID);
            response = _iuserDataProvider.DeleteUser(UserID);
            return Json(response);
        }
    }
}
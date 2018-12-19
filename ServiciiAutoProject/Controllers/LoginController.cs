using DataLayer.Models;
using System;
using ServiciiAuto.DataLayer.Repository;
using System.Web.Mvc;
using ServiciiAutoProject.Helpers;

namespace ServiciiAutoProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User user)
        {
            try
            {
                var correctUser = _loginRepository.Login(user);
                if (correctUser != null)
                {
                    Session["UserId"] = correctUser.Id.ToString();
                    Session["Username"] = correctUser.Username;
                    Session["RoleLevel"] = correctUser.RoleLevel;

                    //log.Info(string.Format("The user: {0} started a new session", Session["Username"]));

                    //var notifications = _dataAccessLayer.GetNotifications();
                    //Session["NotificationNumber"] = notifications.Any() ? notifications.Count : 0;
                    //Session["Notifications"] = notifications.Any() ? notifications : new List<Notification>();
                }
                return Json(new { userName = correctUser != null ? correctUser.Username : string.Empty });
            }
            catch (Exception ex)
            {
                throw ex;
                //log.Error(string.Format("Error in Login \n\r : {0} - {1}", ex.Message, ex.StackTrace));
                //Response.StatusCode = 500;
                //return Json(new { error = _errorHelper.GetErrorMessage(ex) });
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return View("~/Views/Login/Index.cshtml");
        }
    }
}
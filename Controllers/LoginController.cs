using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_Proje.BussinesLogic.Interface.LoginInterface;
using WEB_Proje.Domain.Entities.User;
using WEB_Proje.web.Models.Login;

namespace WEB_Proje.web.Controllers{
    public class LoginController : Controller{

        private readonly IUserLoginInterface userLoginInterface;

        public LoginController() {
            var bl = new BussinesLogic.BussinesLogicClass();
            userLoginInterface = bl.GetUserLogin();
        }

        // Logare
        public ActionResult Login(){
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel user) {
            var dd = new UserDateLogin() {
                Password = user.Password,
                Username = user.Username,
            };
            var userVar = userLoginInterface.ValidateAuth(dd);

            return View();
        }

        // Registrare
        public ActionResult Register() {
            return View();
        }
    }
}
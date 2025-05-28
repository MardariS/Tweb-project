using System.Web.Mvc;
using WEB_Proje.BussinesLogic.Interface.LoginInterface;
using WEB_Proje.BussinesLogic.BlStructure;
using System.Web.Security;
using System.Web;
using System;
using WEB_Proje.Domain.Login;
using WEB_Proje.Domain.Entities.User;
using WEB_Proje.Domain.Enums;
using WEB_Proje.Domain.Models;
using System.Web.UI.WebControls;

namespace WEB_Proje.web.Controllers {
    public class LoginController : Controller {
        private readonly IUserLoginInterface userLoginInterface;
        private readonly LoginBL bl;

        public LoginController() {
            bl = new LoginBL();
            userLoginInterface = bl;
        }

        // GET: Logare
        public ActionResult Login() {
            return View();
        }

        // POST: Logare
        [HttpPost]
        public ActionResult Login(UserDateLogin model) {
            if(!ModelState.IsValid) {
                ViewBag.Error = "Login sau parola incorectă.";
                return View(model);
            }

            if(userLoginInterface.IUserAuthorization(model)) {
                var userInfo = userLoginInterface.ValidateAuth(model);

                if(userInfo != null) {
                    var role = URole.User; 

                    var sessionInfo = new UserSessionInfo {
                        Username = userInfo.Username,
                        Role = role
                    };
                    Session["user"] = sessionInfo;

                    var ticket = new FormsAuthenticationTicket(
                        1,
                        userInfo.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role.ToString(),
                        FormsAuthentication.FormsCookiePath
                    );

                    bl.SetAuthenticationCookies(Response, Request, ticket);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Login sau parola incorectă.";
            return View(model);
        }

        // GET: Register
        public ActionResult Register() {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(UserLoginModel user) {
            if(ModelState.IsValid) {
                if(!userLoginInterface.ValidatePassword(user.Password, user.RepPassword)) {
                    ViewBag.Error = "Parolele nu coincid.";
                    return View(user);
                }

                var userToRegister = new UserDateLogin {
                    Username = user.Username,
                    Password = user.Password
                };

                if(!userLoginInterface.IUserRegistration(userToRegister)) {
                    ViewBag.Error = "Utilizatorul deja există.";
                    return View(user);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

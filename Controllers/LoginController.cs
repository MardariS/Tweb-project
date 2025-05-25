using System.Web.Mvc;
using WEB_Proje.BussinesLogic.Interface.LoginInterface;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.Entities;
using WEB_Proje.Domain.Enums;
using System.Linq;
using WEB_Proje.BussinesLogic.BlStructure;
using System.Web.Security;
using System.Web;
using System;
using WEB_Proje.Domain.Login;
using WEB_Proje.Domain.Entities.User;

namespace WEB_Proje.web.Controllers{
    public class LoginController : Controller{
        // Interfata logarii
        private readonly IUserLoginInterface userLoginInterface;

        // Data de baze
        readonly UserContent db = new UserContent();

        // Logica de logare
        readonly LoginBL logingBL = new LoginBL();

        public LoginController() {
            var bl = new BussinesLogic.BussinesLogicClass();
            userLoginInterface = bl.GetUserLogin();
        }

        // GET: Logare
        public ActionResult Login(){
            return View();
        }

        // POST: Logare
        [HttpPost]
        public ActionResult Login(UserDateLogin model) {
            if(!ModelState.IsValid) {
                return View(model);
            }

            using(var db = new UserContent()) {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if(user != null) {
                    string role = ((URole)user.userRole).ToString();

                    var ticket = new FormsAuthenticationTicket(
                        1,
                        user.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role,
                        FormsAuthentication.FormsCookiePath
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    HttpCookie userLoginInfoCookie = new HttpCookie("UserLoginInfo");
                    userLoginInfoCookie["IP"] = Request.UserHostAddress;
                    userLoginInfoCookie["LoginTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    userLoginInfoCookie.Expires = DateTime.Now.AddDays(7);
                    userLoginInfoCookie.Path = "/";
                    userLoginInfoCookie.Domain = "localhost";
                    Response.Cookies.Add(userLoginInfoCookie);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Login sau parola incorectă.";
            return View(model);
        }

        public ActionResult Register() {
            return View();
        }

        // POST: Registrare
        [HttpPost]
        public ActionResult Register(UserLoginModel user) {
            if(ModelState.IsValid) {
                using(var db = new UserContent()) {
                    var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username);

                    if(existingUser != null) {
                        return View(user);  
                    }

                    if(!logingBL.ValidatePassword(user.Password, user.RepPassword)) {
                        return View(user);
                    }

                    var newUser = new UDdTable {
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email ?? string.Empty,
                        Phone = user.Phone ?? string.Empty,
                        userRole = URole.User
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();
                }


                return RedirectToAction("Index", "Home");

            }

            return View(user);
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
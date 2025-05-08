using System.Web.Mvc;
using WEB_Proje.BussinesLogic.Interface.LoginInterface;
using WEB_Proje.web.Models.Login;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.Entities;
using WEB_Proje.Domain.Enums;
using System.Linq;
using WEB_Proje.BussinesLogic.BlStructure;
using System.Web.Security;
using System.Web;
using System;


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
        public ActionResult Login(string username, string password) {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
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

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Login and password is incorect";
            return View();
        }

        //public ActionResult Login(UserLoginModel user) {
        //    var dd = new UserDateLogin() {
        //        Password = user.Password,
        //        Username = user.Username,
        //    };

        //    var userVar = userLoginInterface.ValidateAuth(dd);

        //    if(userVar == null) {
        //        return View();
        //    }

        //    bool isAuth = userLoginInterface.IUserAuthorization(dd);

        //    if(!isAuth) {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    return RedirectToAction("Index", "Home");
        //}


        // GET: Registrare
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

                    var newUser = new UDdTable() {
                        Username = user.Username,
                        Password = user.Password,
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
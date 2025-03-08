using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_Proje.web.Controllers
{
    public class LoginController : Controller
    {
        // Logarea
        public ActionResult Login()
        {
            return View();
        }

        // Registrarea
        public ActionResult Register() {
            return View();
        }
    }
}
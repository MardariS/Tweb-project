using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_Proje.web.Controllers{
    public class LoginController : Controller{

        // Logare
        public ActionResult Login(){
            return View();
        }

        // Registrare
        public ActionResult Register() {
            return View();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_Proje.web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderPage(){
            return View();
        }
    }
}
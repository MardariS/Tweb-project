﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WEB_Proje.web.App_Start;

namespace WEB_Proje.web
{
    public class Global : HttpApplication{
        void Application_Start(object sender, EventArgs e){
           AreaRegistration.RegisterAllAreas();

            // Bundle
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
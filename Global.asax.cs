using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
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


        //!!!
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(authCookie != null) {
                try {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var identity = new FormsIdentity(ticket);
                    var roles = new[] { ticket.UserData }; 

                    var principal = new System.Security.Principal.GenericPrincipal(identity, roles);
                    HttpContext.Current.User = principal;
                }
                catch {
                    
                }
            }
        }

    }
}
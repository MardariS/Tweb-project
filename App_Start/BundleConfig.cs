using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;



namespace WEB_Proje.web.App_Start {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            // СSS Bundle 
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Style/css/bootstrap.min.css",
                      "~/Content/Style/css/style.css",
                      "~/Content/Style/css/vendor.css"));

            bundles.Add(new StyleBundle("~/bundles/LoginCss").Include(
                      "~/Content/Style/css/styleLogin.css"
                      ));

            // -- JS Header -
            // jQuery
            bundles.Add(new ScriptBundle("~/jquery").Include(
                      "~/Content/Style/js/jquery-1.11.0.min.js"));

            // JS Bundle 
            bundles.Add(new ScriptBundle("~/modernizr").Include(
                      "~/Content/Style/js/modernizr.js"));
    
            // Plugins
            bundles.Add(new ScriptBundle("~/plugins").Include(
                      "~/Content/Style/js/plugins.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                      "~/Content/Style/js/bootstrap.bundle.min.js"));

            // Custom Scripts
            bundles.Add(new ScriptBundle("~/script").Include(
                      "~/Content/Style/js/script.js"));

            // -- JS Footer --
            



            BundleTable.EnableOptimizations = true;
        }
    }
}

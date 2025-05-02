using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_Proje.web.Models.Product;

namespace WEB_Proje.web.Controllers
{
    public class HomeController : Controller{
        public ActionResult Index() {
            var products = ProductRepository.GetAll();
            return View(products);
        }

        public ActionResult AddProduct() {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel model) {
            if(ModelState.IsValid) {
                ProductRepository.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
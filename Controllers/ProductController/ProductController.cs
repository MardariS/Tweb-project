using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WEB_Proje.web.Models.Products;

namespace WEB_Proje.web.Controllers.ProductController {
    public class ProductController : Controller{
        public ActionResult Products() {
            var products = new List<ProductsModel> {
                new ProductsModel { Id = 1, Name = "Iphone 12", Price = 950, OldPrice = 900, ImageURL = "/Content/Style/images/product-item5.jpg" },
            };

            return View(products);
        }
    }
}
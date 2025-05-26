using System;
using System.Linq;
using System.Web.Mvc;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.Product;
using WEB_Proje.BussinesLogic.BlStructure;
using WEB_Proje.BussinesLogic.Core;

    namespace WEB_Proje.web.Controllers {
    public class HomeController : Controller {

        readonly ProductContext db = new ProductContext();

        readonly ProductBL pl = new ProductBL();


        // --- Pagina de adaugare ---
        public ActionResult AddProduct() {
            return View();
        }

        // --- Adaugarea Produs ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel model) {
            var user = BussinesLogic.Session.GetLoggedInUser(this.HttpContext);

            if(user == null) {
                return RedirectToAction("Login", "Login");
            }

            var admin = new AdminAPI(user); 
            var serverRootPath = Server.MapPath("~");

            if(admin.AddProduct(model, serverRootPath, out string error)) {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", error);
            return View(model);
        }


        public ActionResult Index() {
            var products = db.Products.ToList();

            var viewModels = products.Select(p => new ProductModel {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Cantitate = p.Cantitate.ToString(),
                Price = p.Price.ToString("0.##"),
                NewPrice = p.NewPrice?.ToString("0.##"),
                IsOnSale = p.IsOnSale,
                ImagePath = p.ImagePath,
                ImageFileName = p.ImageFileName
            }).ToList();


            return View(viewModels); 
        }


        // ---Stergerea Produsului---
        [HttpPost]
        public ActionResult DeleteProduct(int id) {
            var product = db.Products.Find(id);
            if(product != null) {
                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }  
    }
}

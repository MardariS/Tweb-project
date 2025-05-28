using System.Web.Mvc;
using WEB_Proje.Domain.Product;
using WEB_Proje.BussinesLogic.BlStructure;
using WEB_Proje.BussinesLogic.Core;
using System.Web;

namespace WEB_Proje.web.Controllers {
    public class HomeController : Controller {

        readonly ProductBL pl = new ProductBL();


        // --- Pagina de adaugare ---
        public ActionResult AddProduct() {
            return View();
        }

        // --- Adaugarea Produs ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel model) {
            var user = BussinesLogic.Session.GetLoggedInUser(new HttpContextWrapper(System.Web.HttpContext.Current));

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

        // --- Afișarea Produselor ---
        public ActionResult Index() {
            var products = pl.GetAllProducts();
            return View(products);
        }

        // ---Stergerea Produsului---
        [HttpPost]
        public ActionResult DeleteProduct(int id) {
            var user = BussinesLogic.Session.GetLoggedInUser(new HttpContextWrapper(System.Web.HttpContext.Current));

            if(user == null) {
                return RedirectToAction("Login", "Login");
            }

            var admin = new AdminAPI(user);
            admin.DeleteProduct(id, out _);

            return RedirectToAction("Index");
        }
    }
}

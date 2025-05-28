using System.Web.Mvc;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.ShopStuff;
using WEB_Proje.Domain.Product;
using WEB_Proje.BussinesLogic.BlStructure;
using WEB_Proje.BussinesLogic.Interface.ProductInterface;
using System.Web;
using WEB_Proje.BussinesLogic.Core;
using WEB_Proje.Domain.Entities.User;


namespace WEB_Proje.web.Controllers {
    public class CartController : Controller {

        private readonly IProductInterface _cartService;
        private readonly ProductBL _productBL;

        public CartController() {
            _cartService = new CartLogic(new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session));
            _productBL = new ProductBL();
        }

        public ActionResult CartProducts() {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        public ActionResult RemoveItem(int productId) {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("CartProducts");
        }

        [HttpPost]
        public ActionResult AddToCart(int productId) {
            var productModel = _productBL.GetProductModelById(productId);
            if(productModel == null) {
                return HttpNotFound();
            }

            _cartService.AddToCart(productModel);
            return RedirectToAction("CartProducts");
        }

        public ActionResult ClearCart() {
            _cartService.ClearCart();
            return RedirectToAction("CartProducts");
        }
    }

}
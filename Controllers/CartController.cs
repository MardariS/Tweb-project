using System.Web.Mvc;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.ShopStuff;
using WEB_Proje.Domain.Product;
using WEB_Proje.BussinesLogic.BlStructure;
using WEB_Proje.BussinesLogic.Interface.ProductInterface;
using System.Web;
using WEB_Proje.BussinesLogic.Core;
using WEB_Proje.Domain.Entities.User;


namespace WEB_Proje.web.Controllers

{
    public class CartController : Controller{

        private readonly IProductInterface _cartService;

        readonly ProductContext db = new ProductContext();

        public CartController() {
            _cartService = new CartLogic(new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session));
        }

        // GET: Cart
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
            var product = db.Products.Find(productId);
            if(product == null) {
                return HttpNotFound();
            }

            var productModel = new ProductModel {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.ToString(),
                ImagePath = $"/Content/Images/product-item{product.Id}.jpg"
            };

            _cartService.AddToCart(productModel);
            return RedirectToAction("CartProducts");
        }
    }
}
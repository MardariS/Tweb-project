using System.Web.Mvc;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.ShopStuff;
using WEB_Proje.Domain.Product;


namespace WEB_Proje.web.Controllers

{
    public class CartController : Controller{

        readonly ProductContext db = new ProductContext();

        // GET: Cart
        public ActionResult CartProducts() {
            var cart = Session["Cart"] as ShopStuff ?? new ShopStuff();
            return View(cart);
        }

        public void RemoveItem(int productId) {
            var cart = GetCart(); 
            cart.Items.RemoveAll(i => i.ProductId == productId); 
            SaveCart(cart); 
        }



        [HttpPost]
        public ActionResult AddToCart(int productId) {
            var product = db.Products.Find(productId);
            if(product == null) {
                return HttpNotFound();
            }

            var cart = GetCart();
            var productModel = new ProductModel {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.ToString(),
                ImagePath = $"/Content/Images/product-item{product.Id}.jpg"
            };

            cart.AddItem(productModel);
            SaveCart(cart);

            return RedirectToAction("CartProducts");
        }

        private ShopStuff GetCart() {
            var cart = Session["Cart"] as ShopStuff;
            if(cart == null) {
                cart = new ShopStuff();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCart(ShopStuff cart) {
            Session["Cart"] = cart;
        }
    }
}
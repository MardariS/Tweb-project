using System.Web.Mvc;
using WEB_Proje.BussinesLogic.BlStructure;
using WEB_Proje.BussinesLogic.Interface.ProductInterface;
using WEB_Proje.Domain.Models.Product;

namespace WEB_Proje.web.Controllers {
    public class OrderController : Controller {
        public readonly IOrderInterface _orderInteface;

        public OrderController(IOrderInterface orderInterface) {
            _orderInteface = orderInterface;
        }

        public OrderController() {
            _orderInteface = new OrderLogic();
        }

        [HttpGet]
        public ActionResult OrderPage() {
            return View();
        }

        [HttpPost]
        public ActionResult Order(OrderModel order) {
            if(_orderInteface.OrderClient(order) == false) {
                ViewBag.Message = "Error: ";
                return View("OrderPage");
            }

            ViewBag.Message = "Comanda trimisa!";
            return View("OrderPage");
        }
    }
}

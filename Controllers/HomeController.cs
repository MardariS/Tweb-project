using System;
using System.Linq;
using System.Web.Mvc;
using WEB_Proje.web.Models.Product;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.Entities;
using System.Data.Entity.Validation;

namespace WEB_Proje.web.Controllers {
    public class HomeController : Controller {

        readonly ProductContext db = new ProductContext();

        public ActionResult AddProduct() {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel model) {
            try {
                if(ModelState.IsValid) {
                    if(!decimal.TryParse(model.Price, out decimal price)) {
                        ModelState.AddModelError("Price", "Некорректная цена");
                        return View(model);
                    }

                    decimal? newPrice = null;
                    if(model.IsOnSale) {
                        if(string.IsNullOrEmpty(model.NewPrice) || !decimal.TryParse(model.NewPrice, out decimal parsedNewPrice)) {
                            ModelState.AddModelError("NewPrice", "Укажите корректную цену со скидкой");
                            return View(model);
                        }
                        newPrice = parsedNewPrice;
                    }

                    var entity = new UDdProducts {
                        Name = model.Name,
                        Description = model.Description,
                        Cantitate = int.Parse(model.Cantitate),
                        Price = price,
                        NewPrice = newPrice,
                        IsOnSale = model.IsOnSale
                    };

                    db.Products.Add(entity);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch(DbEntityValidationException ex) {
                foreach(var validationErrors in ex.EntityValidationErrors) {
                    foreach(var validationError in validationErrors.ValidationErrors) {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch(Exception ex) {
                ModelState.AddModelError("", "Произошла ошибка: " + ex.Message);
            }

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
                IsOnSale = p.IsOnSale && p.NewPrice.HasValue, 
                ImagePath = $"/Content/Images/{p.Id}.jpg"
            }).ToList();

            ViewBag.SaleProducts = viewModels.Where(p => p.IsOnSale).ToList();
            ViewBag.NormalProducts = viewModels.Where(p => !p.IsOnSale).ToList();

            return View(viewModels);
        }


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

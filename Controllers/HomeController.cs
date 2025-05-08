using System;
using System.Linq;
using System.Web.Mvc;
using WEB_Proje.web.Models.Product;
using WEB_Proje.BussinesLogic.DBModel;
using WEB_Proje.Domain.Entities;
using System.IO;
using System.Data.Entity.Validation;

namespace WEB_Proje.web.Controllers {
    public class HomeController : Controller {

        readonly ProductContext db = new ProductContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct() {
            return View();
        }


        // --- Adaugarea Produs ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel model) {
            try {
                if(!ModelState.IsValid) {
                    return View(model);
                }

                if(!int.TryParse(model.Cantitate, out int cantitate) || cantitate <= 0) {
                    ModelState.AddModelError("Cantitate", "Cantitate invalidă (trebuie să fie număr pozitiv)");
                    return View(model);
                }

                if(!decimal.TryParse(model.Price, out decimal price) || price <= 0) {
                    ModelState.AddModelError("Price", "Preț invalid (trebuie să fie număr pozitiv)");
                    return View(model);
                }

                decimal? newPrice = null;
                if(model.IsOnSale) {
                    if(string.IsNullOrEmpty(model.NewPrice) || !decimal.TryParse(model.NewPrice, out decimal parsedNewPrice) || parsedNewPrice <= 0) {
                        ModelState.AddModelError("NewPrice", "Preț reducere invalid");
                        return View(model);
                    }
                    newPrice = parsedNewPrice;
                }

                string imageFileName = null;
                if(!string.IsNullOrEmpty(model.ImagePath)) {
                    var serverPath = Server.MapPath(model.ImagePath);
                    if(!System.IO.File.Exists(serverPath)) {
                        ModelState.AddModelError("ImagePath", "Fișierul nu există la calea specificată");
                        return View(model);
                    }
                    imageFileName = Path.GetFileName(model.ImagePath);
                }

                var product = new UDdProducts {
                    Name = model.Name,
                    Description = model.Description,
                    Cantitate = cantitate,
                    Price = price,
                    NewPrice = newPrice,
                    IsOnSale = model.IsOnSale,
                    ImagePath = model.ImagePath,
                    ImageFileName = imageFileName
                };

                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(DbEntityValidationException ex) {
                foreach(var validationErrors in ex.EntityValidationErrors) {
                    foreach(var validationError in validationErrors.ValidationErrors) {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return View(model);
            }
            catch(Exception ex) {
                var message = ex.InnerException?.InnerException?.Message ?? ex.Message;
                throw new Exception("Ошибка при сохранении: " + message);
            }
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

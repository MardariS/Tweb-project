using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Proje.web.Models.Products {
    public class ProductsModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice {  get; set; }
        public string ImageURL { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Proje.web.Models.Product {
    public class CartStufff {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
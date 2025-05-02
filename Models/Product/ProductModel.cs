using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Proje.web.Models.Product {
    public class ProductModel {
        public int Id { get; set; } // 1
        public string Name { get; set; } // Iphone
        public string Description { get; set; } // Iprum Leprum...
        public string Cantitate { get; set; } // 23
        public bool IsOnSale { get; set; } // False
        public string Price { get; set; } // 853$
        public string NewPrice { get; set; } // 799$
        public string ImagePath { get; set; } // ~/Content/Style/images
    }
}
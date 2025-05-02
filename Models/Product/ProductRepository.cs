using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Proje.web.Models.Product {
    public static class ProductRepository {
        private static List<ProductModel> products = new List<ProductModel>();

        public static List<ProductModel> GetAll() => products;

        public static void Add(ProductModel product) {
            product.Id = products.Count + 1;
            products.Add(product);
        }
    }
}

using System.ComponentModel.DataAnnotations;


namespace WEB_Proje.web.Models.Product {
    public class ProductModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Cantitatea este obligatorie")]
        public string Cantitate { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu")]
        public string Price { get; set; }

        public bool IsOnSale { get; set; }

        [Display(Name = "Preț reducere")]
        public string NewPrice { get; set; }

        [Display(Name = "Cale imagine (opțional)")]
        public string ImagePath { get; set; }

        public string ImageFileName { get; set; }
    }

}
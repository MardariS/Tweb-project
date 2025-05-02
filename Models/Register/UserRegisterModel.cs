using System.ComponentModel.DataAnnotations;


namespace WEB_Proje.web.Models.Register {
    public class UserRegisterModel {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parola nu coencide" ) ]
        public string RepeatPassword { get; set; }
    }
}
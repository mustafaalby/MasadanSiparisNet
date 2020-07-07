using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class NewRestaurantSignUpViewModel
    {
        [Required(ErrorMessage = "Mail Adresinizi Giriniz")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adınızı Giriniz")]
        [StringLength(10, ErrorMessage = "Kullanıcı adınız en fazla 10 karakter olabilir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrenizi Giriniz")]
        [StringLength(10, ErrorMessage = "Şifre uzunluğu en fazla 10 karakter olabilir")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor")]

        public string PasswordConfirm { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class RestaurantSignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adını giriniz")]
        [StringLength(10, ErrorMessage = "Kullanıcı adınız en fazla 10 karakter olabilir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifrenizi Giriniz")]
        [StringLength(10, ErrorMessage = "Şifre uzunluğu en fazla 10 karakter olabilir")]
        public string Password { get; set; }
    }
}

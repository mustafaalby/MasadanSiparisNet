using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class RestaurantAdresViewModel
    {
        public int AddressId { get; set; }
        [Required(ErrorMessage ="Şehir Bilgisi Giriniz")]
        public string City { get; set; }
        [Required(ErrorMessage ="İlçe Bilgisi Giriniz")]
        public string District { get; set; }
        [Required(ErrorMessage ="Mahalle Bilgisi Giriniz")]
        public string Neighborhood { get; set; }
        [Required(ErrorMessage ="Sokak ve Numara Bilgisi Giriniz")]
        public string StreetAndNu { get; set; }
    }
}

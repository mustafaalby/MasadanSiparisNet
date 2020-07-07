using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class RestaurantEditViewModel
    {
        [Required(ErrorMessage = "Email Adresinizi Giriniz")]
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }
        [Required(ErrorMessage = "Numaranızı Giriniz")]
        [Phone]
        public string PhoneNumber{get; set;}

        [Required(ErrorMessage ="Restoran Adı Giriniz")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage ="Çalışan Sayısı Giriniz")]
        [Range(1,int.MaxValue,ErrorMessage ="Kabul Edilebilir Aralıkta Değer Giriniz")]
        public int AmountOfWorkers { get; set; }

        public  RestaurantAdresViewModel RestaurantAddress { get; set; }
    }
}

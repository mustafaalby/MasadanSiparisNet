using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;
        private readonly RestaurantService _restaurantService;
        public ContactController(ContactService contactService, RestaurantService restaurantService)
        {
            _contactService = contactService;
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            var restInfo = await _restaurantService.GetRestaurantInfo(User.Identity.Name);
            RestaurantAdresViewModel adress = new RestaurantAdresViewModel
            {
                AddressId = restInfo.RestaurantAddress.AddressId,
                City = restInfo.RestaurantAddress.City,
                District = restInfo.RestaurantAddress.District,
                Neighborhood = restInfo.RestaurantAddress.Neighborhood,
                StreetAndNu = restInfo.RestaurantAddress.StreetAndNu,
            };
            return View(adress);
        }

        [HttpPost]
        public ActionResult Index(ContactMailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + model.Name);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Subject);
                body.AppendLine("Mesaj: " + model.Message);
                _contactService.MailSender(body.ToString());
            }
            return View();
        }
    }
}
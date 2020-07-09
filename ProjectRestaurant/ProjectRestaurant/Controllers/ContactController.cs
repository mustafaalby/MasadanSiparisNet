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

        public  async Task< IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var restInfo1 =await _restaurantService.GetRestaurantInfo(User.Identity.Name);    
                RestaurantAdresViewModel adress1 = new RestaurantAdresViewModel
                {
                    AddressId = restInfo1.RestaurantAddress.AddressId,
                    City = restInfo1.RestaurantAddress.City,
                    District = restInfo1.RestaurantAddress.District,
                    Neighborhood = restInfo1.RestaurantAddress.Neighborhood,
                    StreetAndNu = restInfo1.RestaurantAddress.StreetAndNu,
                };
                ViewBag.Email = restInfo1.Email;
                ViewBag.PhoneNumber = restInfo1.PhoneNumber;
                ViewData["GoogleMapApiKey"] = "AIzaSyDF_dzWQK0NUe3px2y31Qs_ejSKKZB5k2U";

                return View(adress1);
            }
            else if (Request.Cookies["SessionId"] == null)
            {
                return RedirectToAction("Tables", "Home");
            }
            int sessionId = int.Parse(Request.Cookies["SessionId"]);
            var restInfo =  _restaurantService.GetRestaurantContectInfo(sessionId);
            RestaurantAdresViewModel adress = new RestaurantAdresViewModel
            {
                AddressId = restInfo.Table.Restaurant.RestaurantAddress.AddressId,
                City = restInfo.Table.Restaurant.RestaurantAddress.City,
                District = restInfo.Table.Restaurant.RestaurantAddress.District,
                Neighborhood = restInfo.Table.Restaurant.RestaurantAddress.Neighborhood,
                StreetAndNu = restInfo.Table.Restaurant.RestaurantAddress.StreetAndNu,
            };
            ViewBag.Email = restInfo.Table.Restaurant.Email;
            ViewBag.PhoneNumber = restInfo.Table.Restaurant.PhoneNumber;
            ViewData["GoogleMapApiKey"] = "AIzaSyDF_dzWQK0NUe3px2y31Qs_ejSKKZB5k2U";

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
                TempData["Message"] = "Mesajınız başarılı bir şekilde gönderildi.";
            }
            return RedirectToAction("Index");
        }
    }
}
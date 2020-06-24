using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class SessionController : Controller
    {
        private RestaurantService _restaurantService;
        private IMapper _mapper;
        public SessionController(RestaurantService service, IMapper mapper)
        {
            _restaurantService = service;
            _mapper = mapper; 
        }
        public IActionResult Index()
        {

            return View();
        }
        public async Task< IActionResult> SessionRequest(int id)
        {
            int SessionId= await _restaurantService.OpenNewSession(id);
            HttpContext.Session.SetInt32("SessionId", SessionId);
            TempData["Table"] = HttpContext.Session.GetInt32("SessionId");
            return RedirectToAction("Privacy", "Home");//Müşterinin Menüyü görme sayfaları eklendiğinde, menü sayfasına yönlendirilecek
        }
    }
}
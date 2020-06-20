using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Dto;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantService restaurantService,IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewRestaurant()
        {

            return View();
        }
        [HttpPost]
        public  async  Task<IActionResult> NewRestaurant(NewRestaurantSignUpViewModel model)
        {
            var mapped = _mapper.Map<NewRestaurantDto>(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _restaurantService.NewRestaurant(mapped);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LoginRestaurant));
            }
            return View();
        }
        public IActionResult LoginRestaurant()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginRestaurant(RestaurantSignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mapped = _mapper.Map<RestoranLoginDto>(model);
            
            var result = await _restaurantService.Login(mapped);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            _restaurantService.Logout();
            return RedirectToAction(nameof(LoginRestaurant));
        }
        public async Task<IActionResult> EditRestaurant()
        {
            var rest = await _restaurantService.GetRestaurantInfo(User.Identity.Name);
            var mapped = _mapper.Map<RestaurantEditViewModel>(rest);
            return View(mapped);
        }
        [HttpPost]
        public async Task<IActionResult> EditRestaurant(RestaurantEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mapped = _mapper.Map<Restaurant>(model);
            var result= await _restaurantService.UpdateRestaurantInfo(mapped);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
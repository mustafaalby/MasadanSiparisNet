using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Hubs;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Dto;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;
        private readonly TableService _tableService;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantService restaurantService, TableService tableService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _tableService = tableService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var tables = _tableService.GetAllTables();
            var mapped = _mapper.Map<List<TableViewModel>>(tables);
            
            return View(mapped);
        }
        public IActionResult NewRestaurant()
        {
            var result= _tableService.GetAllTables();
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

        public IActionResult NewTable()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> NewTable(NewTableViewModel model)
        {
            var mapped = _mapper.Map<Table>(model);
            await _tableService.AddNewTable(mapped,User.Identity.Name);
            return RedirectToAction(nameof(Tables));
        }
        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableService.DeleteTable(id);
            return RedirectToAction(nameof(Tables));
        }

        public IActionResult EditTable(int id)
        {
            var table = _tableService.GetTableById(id);
            var mapped = _mapper.Map<EditTableViewModel>(table);
            return View(mapped);
        }
        [HttpPost]
        public async Task<IActionResult> EditTable(EditTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mapped = _mapper.Map<Table>(model);
            var result =await _tableService.EditTable(mapped);
            if (result > 0)
            {
                return RedirectToAction(nameof(Tables));
            }
            return View(model);
        }
        public IActionResult Tables()
        {
            var tables = _tableService.GetAllTables();
            var mapped = _mapper.Map<List<TableViewModel>>(tables);
            return View(mapped);
        }
    }
}
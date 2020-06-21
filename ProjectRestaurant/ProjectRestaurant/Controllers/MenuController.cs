using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;
        private readonly IMapper _mapper;
        public MenuController(MenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var menuModel = _menuService.GetMenu();
            var mapped = _mapper.Map<List<MenuViewModel>>(menuModel);
            return View(mapped);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Add()
        {
            var productTypes = _menuService.GetProductTypes().ToList().Select(x => new SelectListItem()
            {
                Value = x.ProductTypeId.ToString(),
                Text = x.Type
            }).ToList();

            ViewBag.ProductTypes = productTypes;

            return PartialView();
        }

        [HttpPost]
        public IActionResult Add(MenuViewModel model)
        {
            var menuContent = _mapper.Map<Menu>(model);
            _menuService.AddNewMenuContent(menuContent);
            return Ok();
        }
    }
}
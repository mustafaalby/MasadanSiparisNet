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

        public IActionResult Update(int id)
        {
            var menuContent = _menuService.GetMenuContentById(id);
            var mapped = _mapper.Map<MenuViewModel>(menuContent);

            var productTypes = _menuService.GetProductTypes().ToList().Select(x => new SelectListItem()
            {
                Value = x.ProductTypeId.ToString(),
                Text = x.Type
            }).ToList();

            ViewBag.ProductTypes = productTypes;

            return PartialView(mapped);
        }

        [HttpPost]
        public IActionResult Update(MenuViewModel model)
        {
            var mapped = _mapper.Map<Menu>(model);
            _menuService.UpdateMenuContent(mapped);
            return Ok();
        }

        public IActionResult Delete(int id)
        {
            _menuService.DeleteMenuContent(id);
            return Ok();
        }

        public IActionResult Index1()
        {
            var productTypes = _menuService.GetProductTypes();
            var mapped = _mapper.Map<List<ProductTypeViewModel>>(productTypes);


            return View(mapped);
        }
        public IActionResult GetProduct(int id)
        {
            var product = _menuService.GetProductTypeById(id);
            var mapped = _mapper.Map<List<MenuViewModel>>(product);

            return View(mapped);
        }
    }
}
    

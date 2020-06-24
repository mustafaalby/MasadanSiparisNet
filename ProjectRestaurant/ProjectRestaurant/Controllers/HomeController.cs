using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantService _restaurantService;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IMapper mapper, RestaurantService service)
        {
            _mapper = mapper;
            _restaurantService = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tables = _restaurantService.GetAllTables();
            var mapped = _mapper.Map<List<TableViewModel>>(tables);

            return View(mapped);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

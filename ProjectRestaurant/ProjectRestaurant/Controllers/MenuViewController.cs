using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectRestaurant.Data.Context;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Dto;
using ProjectRestaurant.Service.Service;

namespace ProjectRestaurant.Controllers
{
    public class MenuViewController : Controller
    {
         
        private readonly MenuService _service;
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        public MenuViewController(MenuService service, RestaurantDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["SessionId"]== null){
                return RedirectToAction("Tables", "Home");
            }
            var sessionId =int.Parse( Request.Cookies["SessionId"]);
            var result= _service.CheckIfTableIsAvaibleBySessionId(sessionId);

            if (result == true)
            {
                Response.Cookies.Delete("SessionId");
                return RedirectToAction("Tables", "Home");
                //Eğer müşteri sayfasını kapattıktan sonra Restoran hesabı kapatmışsa, müşteri bağlandığında bunun kontrolü yapılır
            }

            var productTypes = _service.GetProductTypes();
            var mapped = _mapper.Map<List<ProductTypeViewModel>>(productTypes);
            var table = _service.GetTableBySessionId(int.Parse(Request.Cookies["SessionId"]));
            TempData["SessionId"] = Request.Cookies["SessionId"];
            ViewBag.tableId = table.TableId;
            ViewBag.tableName = table.TableName;

            return View(mapped);
        }
        public IActionResult GetProduct(int id)
        {
            var product = _service.GetProductTypeById(id);
            var mapped = _mapper.Map<List<MenuViewModel>>(product);

            return View(mapped);
        }
        public async Task< IActionResult> Order(List<OrderViewModel> order)
        {
            if (Request.Cookies["SessionId"]==null)
            {
                return RedirectToAction(nameof(Index));
            }
            int session = int.Parse( Request.Cookies["SessionId"]);
            var mapped = _mapper.Map<List<OrderDto>>(order);
            await _service.MakeOrder(mapped, session);
            return Ok();
        }
        public  IActionResult MySession()
        {
            if (Request.Cookies["SessionId"]==null)
            {
                return RedirectToAction(nameof(Index));
            }
            int session =int.Parse( Request.Cookies["SessionId"]);
            var ses=_service.MySession(session);
            var mapped = _mapper.Map<SessionViewModel>(ses);
            return View(mapped);
        }
        public IActionResult CloseSession()
        {
            
            Response.Cookies.Delete("SessionId");
            return RedirectToAction("Tables","Home");
        }
    }
}

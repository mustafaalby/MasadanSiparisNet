using Microsoft.EntityFrameworkCore;
using ProjectRestaurant.Data.Context;
using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRestaurant.Service.Service
{
    public class MenuService
    {
        private readonly RestaurantDbContext _context;
        public MenuService(RestaurantDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all menu contents
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetMenu()
        {
            var menu = _context.Set<Menu>().ToList();
            return menu;
        }
    }
}

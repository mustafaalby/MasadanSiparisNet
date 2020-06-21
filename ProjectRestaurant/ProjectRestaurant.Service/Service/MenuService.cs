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

        /// <summary>
        /// Get all product types
        /// </summary>
        /// <returns></returns>
        public List<ProductType> GetProductTypes()
        {
            var productTypes = _context.Set<ProductType>().ToList();
            return productTypes;
        }

        /// <summary>
        /// add new menu content to db table
        /// </summary>
        /// <param name="menuContent"></param>
        public void AddNewMenuContent(Menu menuContent)
        {
            _context.Set<Menu>().Add(menuContent);
            _context.SaveChanges();
        }
    }
}

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

        /// <summary>
        /// get product from db according to MenuId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu GetMenuContentById(int id)
        {
            return _context.Set<Menu>().FirstOrDefault(x => x.MenuId == id);
        }

        /// <summary>
        /// Update existed product info in menu table
        /// </summary>
        /// <param name="menuContent"></param>
        public void UpdateMenuContent(Menu menuContent)
        {
            _context.Set<Menu>().Update(menuContent);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete product from menu table
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMenuContent(int id)
        {
            var menuContent = _context.Set<Menu>().FirstOrDefault(x => x.MenuId == id);
            _context.Remove(menuContent);
            _context.SaveChanges();
        }
    }
}

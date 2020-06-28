using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectRestaurant.Data.Context;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public int GetTableBySessionId(int id)
        {
            return _context.Session.Where(x => x.SessionId == id).FirstOrDefault().Table.TableId;
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
        public List<Menu> GetProductTypeById(int id)
        {
            return _context.Set<Menu>().Where(x => x.ProductTypeId == id).ToList();
        }
        public async Task MakeOrder(List<OrderDto> order ,int session)
        {
            float totalFee=0;
            foreach (var item in order)
            {
                totalFee += (item.Price * item.Quantity);
            }
            var Ses= _context.Session.Where(x => x.SessionId == session).FirstOrDefault();
            var oldOrders = _context.Order.Where(x => x.SessionId == session).ToList();
            foreach (var item in oldOrders)
            {
                totalFee+=(item.Price*item.Quantity);
            }
            
            foreach (var item in order)
            {
                Order orderItem = new Order
                {
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SessionId = session,
                    Session = Ses
                };
                _context.Order.Add(orderItem);
            }
            Ses.TotalFee = totalFee;
            _context.Session.Update(Ses);
            await _context.SaveChangesAsync();
        }
        public SessionDto MySession(int sessionId)
        {
            var session= _context.Session.Where(x => x.SessionId == sessionId).FirstOrDefault();
            var table = _context.Table.Where(x => x.TableId == session.TableId).FirstOrDefault();
            SessionDto sessionDto = new SessionDto
            {
                 SessionId=session.SessionId,
                 StartDate=session.StartDate,
                 FinishDate=session.FinishDate,
                 TableName=table.TableName,
                 TableId=table.TableId,
                 TotalFee=session.TotalFee,
                 Order=session.Order
            };
            return sessionDto;
        }
    }
}

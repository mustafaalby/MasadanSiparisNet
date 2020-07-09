using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

using Microsoft.EntityFrameworkCore;
using ProjectRestaurant.Data.Context;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRestaurant.Service.Service
{
    public class RestaurantService
    {
        private readonly UserManager<Restaurant> _userManager;
        private readonly SignInManager<Restaurant> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RestaurantDbContext _context;

        public RestaurantService(
            UserManager<Restaurant> userManager,
            SignInManager<Restaurant> signInManager,
            RoleManager<IdentityRole> roleManager,
            RestaurantDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

       

        public async Task<IdentityResult> NewRestaurant(NewRestaurantDto model)
        {
            IdentityResult result = new IdentityResult();
            var user = new Restaurant
            {
                Email = model.Email,
                UserName = model.UserName
            };
            result = await _userManager.CreateAsync(user, model.Password);
            var temp = await _userManager.FindByNameAsync(user.UserName);
            RestaurantAddress adres = new RestaurantAddress
            {
                Restaurant = temp,
                RestaurantId = user.Id
            };
            _context.Set<RestaurantAddress>().Add(adres);
            await _context.SaveChangesAsync();
            return result;

        }
        public async Task<SignInResult> Login(RestoranLoginDto model)
        {
            SignInResult result = new SignInResult();

            var user = await _userManager.FindByNameAsync(model.UserName);
            result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            return result;
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
       
        public async Task<Restaurant> GetRestaurantInfo(string userName)
        {
            var restaurant = await _userManager.FindByNameAsync(userName);

            return restaurant;
        }
        public Session GetRestaurantContectInfo(int SessionId)
        {
            var Session = _context.Session.Where(x => x.SessionId == SessionId).FirstOrDefault();

            return Session;
        }
        public async Task<IdentityResult> UpdateRestaurantInfo(Restaurant model)
        {
            var rest = await _userManager.FindByNameAsync(model.UserName);
            rest.RestaurantAddress = model.RestaurantAddress;
            rest.AmountOfWorkers = model.AmountOfWorkers;
            rest.Email = model.Email;
            rest.PhoneNumber = model.PhoneNumber;
            rest.RestaurantName = model.RestaurantName;
            rest.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(rest);
            return result;
        }

        public async Task<int> AddNewTable(Table model,string userName)
        {
            var rest =await _userManager.FindByNameAsync(userName);
            model.Restaurant = rest;
            model.RestaurantId = rest.Id;
            model.IsAvailable = true;
            _context.Set<Table>().Add(model);
            var result=await _context.SaveChangesAsync();
            return result;
        }
        public List<Table> GetAllTables()
        {
            
            var result =_context.Set<Table>().ToList();
            return result;
        }
        public async Task<int> DeleteTable(int id)
        {
            var model = _context.Table.Where(x => x.TableId == id).FirstOrDefault();
            _context.Table.Remove(model);
            var result = await _context.SaveChangesAsync();
            return result;
            
        }

        public Table GetTableById(int id)
        {
            var table =  _context.Table.Where(x => x.TableId == id).FirstOrDefault();
            return table;
        }
        public async Task<int> EditTable(Table table)
        {

            var tab = _context.Table.Where(x => x.TableId == table.TableId).FirstOrDefault();
            tab.TableName = table.TableName;
            _context.Set<Table>().Update(tab);
            var result= await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> OpenNewSession(int tableId)
        {
            Table table = _context.Table.Where(x => x.TableId == tableId).FirstOrDefault();
            table.IsAvailable = false;
            _context.Table.Update(table);
            Session newSession = new Session
            {
                StartDate = DateTime.Now,
                Table = table,
                TableId = tableId
            };
            _context.Session.Add(newSession);
            await _context.SaveChangesAsync();
            var x = _context.Session.OrderByDescending(x => x.SessionId).FirstOrDefault();
            return x.SessionId;
        }
        public SessionDto GetSessionDetail(int id)
        {
            var sessions = _context.Session.Where(x => x.TableId == id).OrderByDescending(x=>x.SessionId).FirstOrDefault();
            
            SessionDto sessionDto = new SessionDto
            {
                FinishDate = sessions.FinishDate,
                Order = sessions.Order,
                SessionId = sessions.SessionId,
                StartDate = sessions.StartDate,
                TotalFee = sessions.TotalFee,
                TableId=sessions.TableId,
                TableName = sessions.Table.TableName
            };
            sessionDto.Order= sessionDto.Order.OrderByDescending(x => x.OrderId).ToList();
            return sessionDto;
        }

        public void CloseSession(int id)
        {
            var session = _context.Session.Where(x => x.SessionId == id).FirstOrDefault();
            var table = _context.Table.Where(x => x.TableId == session.TableId).FirstOrDefault();
            table.IsAvailable = true;
            _context.Table.Update(table);
            _context.SaveChanges();
        }

        public void DeliverOrder(int id)
        {
            var orders = _context.Order.Where(x => x.SessionId == id);
            foreach (var item in orders)
            {
                item.isDelivered = true;
            }
            foreach (var item in orders)
            {
                _context.Order.Update(item);
            }
            _context.SaveChanges();
        }
    }
}

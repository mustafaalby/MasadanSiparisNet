using Microsoft.AspNetCore.Identity;
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
    public class TableService
    {
        private readonly UserManager<Restaurant> _userManager;
        private readonly SignInManager<Restaurant> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RestaurantDbContext _context;
        public TableService(
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
        public async Task<List<Table>> TableList(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var masalar = _context.Set<Table>().Where(x => x.RestaurantId == user.Id).ToList();
            return masalar;
        }
        public async void AddTable(Table masa, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            masa.Restaurant = user;
            masa.RestaurantId = user.Id;
            _context.Set<Table>().Add(masa);
            var result = await _context.SaveChangesAsync();
        }
        public async Task<int> AddNewTable(Table model, string userName)
        {
            if (isSameTableNameExist(model.TableName))
                return 0;
            else
            {
                var rest = await _userManager.FindByNameAsync(userName);
                model.Restaurant = rest;
                model.RestaurantId = rest.Id;
                model.IsAvailable = true;
                _context.Set<Table>().Add(model);
                var result = await _context.SaveChangesAsync();
                return result;
            }
        }
        public List<Table> GetAllTables()
        {

            var result = _context.Set<Table>().ToList();
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
            var table = _context.Table.Where(x => x.TableId == id).FirstOrDefault();
            return table;
        }
        public async Task<int> EditTable(Table table)
        {
            if (isSameTableNameExist(table.TableName))
                return 0;
            else
            {
                var tab = await _context.Table.Where(x => x.TableId == table.TableId).FirstOrDefaultAsync();
                tab.TableName = table.TableName;
                _context.Set<Table>().Update(tab);
                var result = await _context.SaveChangesAsync();
                return result;
            }
        }

        private bool isSameTableNameExist(string tableName)
        {
            return _context.Table.Where(x => x.TableName == tableName).FirstOrDefault() != null ? true : false;
        }
    }
}

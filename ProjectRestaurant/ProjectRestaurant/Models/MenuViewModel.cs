using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}

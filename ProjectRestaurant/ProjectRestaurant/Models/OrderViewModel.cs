using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class OrderViewModel
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        
    }
}

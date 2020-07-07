using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRestaurant.Service.Dto
{
    public class OrderDto
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
    }
}

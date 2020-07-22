using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRestaurant.Service.Dto
{
   public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
    }
}

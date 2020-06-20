using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}

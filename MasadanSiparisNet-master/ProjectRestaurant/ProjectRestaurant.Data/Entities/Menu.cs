using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public virtual  ProductType ProductType {get;set;}
    }
}

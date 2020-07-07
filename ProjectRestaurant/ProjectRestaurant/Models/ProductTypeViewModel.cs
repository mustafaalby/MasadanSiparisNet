using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class ProductTypeViewModel
    {
        
        public int ProductTypeId { get; set; }
        public string Type { get; set; }
        public List<Menu> Menu { get; set; }
    }
}

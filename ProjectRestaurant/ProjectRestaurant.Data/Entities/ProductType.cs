using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set;}
        public string Type { get; set; }
        public virtual Menu Menu { get; set; }
    }
}

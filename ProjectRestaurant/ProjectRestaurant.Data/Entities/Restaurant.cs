using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Restaurant:IdentityUser
    {        
        public string RestaurantName { get; set; }
        public int AmountOfWorkers { get; set; }
        public virtual RestaurantAddress RestaurantAddress{get;set;}
        public virtual ICollection<Table> Table { get; set; }

    }
}

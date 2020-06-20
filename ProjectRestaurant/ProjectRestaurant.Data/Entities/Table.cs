using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string RestaurantId { get; set; }
        public bool IsAvailable { get; set; }
        public virtual Restaurant Restaurant{get;set;}  
        public virtual ICollection<Session> Sessions { get; set; }
    }
}

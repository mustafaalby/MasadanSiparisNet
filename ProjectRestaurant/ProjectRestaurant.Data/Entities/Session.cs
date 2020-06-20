using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public float TotalFee { get; set; }
        public int TableId { get; set; }
        public virtual Table Table {get;set;}
        public virtual ICollection<Order> Order { get; set; }
    }
}

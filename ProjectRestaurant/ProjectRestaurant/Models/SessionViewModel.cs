using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class SessionViewModel
    {
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public float TotalFee { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public  List<Order> Order { get; set; }
    }
}

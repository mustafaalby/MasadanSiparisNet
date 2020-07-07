using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRestaurant.Service.Dto
{
    public class SessionDto
    {
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public float TotalFee { get; set; }
        public bool isDelivered { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public  ICollection<Order> Order { get; set; }
    }
}

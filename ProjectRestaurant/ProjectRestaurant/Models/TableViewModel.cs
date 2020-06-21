using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class TableViewModel
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

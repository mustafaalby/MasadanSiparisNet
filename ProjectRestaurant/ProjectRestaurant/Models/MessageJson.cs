using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class MessageJson
    {
        public int SessionId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string MessageText { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectRestaurant.Data.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public virtual Session Session { get; set; }
        public int SessionId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string MessageText { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class EditTableViewModel
    {
        public int TableId { get; set; }
        [Required(ErrorMessage ="Masa Adı Boş Bırakılamaz")]
        public string TableName { get; set; }
    }
}

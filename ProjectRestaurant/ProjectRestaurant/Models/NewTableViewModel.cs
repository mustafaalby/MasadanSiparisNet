using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Models
{
    public class NewTableViewModel
    {
        [Required(ErrorMessage ="Masa Adı Boş Bırakılamaz")]
        public string TableName { get; set; }
    }
}

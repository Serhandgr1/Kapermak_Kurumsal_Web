using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class ReferanceEntities
    {
        [Key]
        public int Id{ get; set; }
        public string  ReferangeName { get; set; }
        public string? ReferangeDetail { get; set; }
        public  string? ReferangeImage { get; set; }
    }
}

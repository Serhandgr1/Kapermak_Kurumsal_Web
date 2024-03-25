using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class ServicesEntities
    {
        [Key]
        public int Id { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDetail { get; set; }
        public string ServiceImage { get; set; }
    }
}

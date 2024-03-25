using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class PreferenceEntiites
    {
        [Key]
        public int Id{ get; set; }
        public string PreferenceTitle { get; set; }
        public string PreferenceDetail { get; set; }
        public string PreferenceImage { get; set; }
    }
}

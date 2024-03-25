using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class PreferenceDTO
    {
        public int Id { get; set; }
        public string PreferenceTitle { get; set; }
        public string PreferenceDetail { get; set; }
        public string PreferenceImage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewPreferanceDto:PreferenceDTO
    {
        public string EnPreferenceTitle { get; set; }
        public string EnPreferenceDetail { get; set; }
    }
}

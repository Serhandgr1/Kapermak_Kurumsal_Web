using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class CategoryClientDto:CategoryDTO
    {
        public string TrLangue { get; set; }
        public string EnLangue { get; set; }
    }
}

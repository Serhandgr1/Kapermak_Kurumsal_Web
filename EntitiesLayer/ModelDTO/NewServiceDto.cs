using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewServiceDto :ServicesDTO
    {
        public string EnServiceTitle { get; set; }
        public string EnServiceDetail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class CategoryDTO
    {
        public int Id{ get; set; }
        public string CategoryName { get; set; }
        public bool? CategoryProduct { get; set; }
    }
}

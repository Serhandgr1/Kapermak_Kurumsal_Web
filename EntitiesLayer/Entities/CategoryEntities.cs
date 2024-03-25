using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class CategoryEntities
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}

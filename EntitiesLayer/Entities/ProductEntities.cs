using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class ProductEntities
    {
        [Key]
        public int Id { get; set; }
        public string ProductDetail { get; set; }
        public string? ProductSpacial { get; set; }
        public string ProductTitle { get; set; }

        public string ProductImage { get; set; }
        public string? ProductImage2 { get; set; }
        public string? ProductImage3 { get; set; }
        [ForeignKey("CategoryEntities")]
        public int CategoryId { get; set; }

    }
}

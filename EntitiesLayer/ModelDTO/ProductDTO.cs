using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class ProductDTO
    {

        public int? Id { get; set; }
        public string ProductDetail { get; set; }
        public string? ProductSpacial { get; set; }
        public string ProductTitle { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string? ProductImage2 { get; set; }
        public string? ProductImage3 { get; set; }
    }
}

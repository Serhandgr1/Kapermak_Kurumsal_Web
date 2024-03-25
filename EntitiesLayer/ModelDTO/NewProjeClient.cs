using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewProjeClient
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string EnProjectTitle { get; set; }
        public string ProjectDetail { get; set; }
        public string EnProjectDetail { get; set; }
        public IFormFile ProjectImage { get; set; }
        public string Image { get; set; }
    }
}

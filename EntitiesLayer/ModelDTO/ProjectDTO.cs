using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDetail { get; set; }
        public string ProjectImage { get; set; }
    }
}

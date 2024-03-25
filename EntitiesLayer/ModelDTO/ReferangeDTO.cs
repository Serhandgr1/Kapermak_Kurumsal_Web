using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class ReferangeDTO
    {
        public int Id { get; set; }
        public string ReferangeName { get; set; }
        public string? ReferangeDetail { get; set; }
        public string? ReferangeImage { get; set; }
    }
}

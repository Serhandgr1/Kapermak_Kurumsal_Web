using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewReferanceClient
    {
        public int Id { get; set; }
        public string ReferangeName { get; set; }
        public string? ReferangeDetail { get; set; }
        public IFormFile? ReferangeImage { get; set; }
    }
}

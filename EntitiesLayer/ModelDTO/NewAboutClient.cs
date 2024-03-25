using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewAboutClient
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public string EnContents { get; set; }
        public string Title { get; set; }
        public string EnTitle { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public string ImageClient { get; set; }
        public string ImageClient2 { get; set; }
        public string ImageClient3 { get; set; }
    }
}

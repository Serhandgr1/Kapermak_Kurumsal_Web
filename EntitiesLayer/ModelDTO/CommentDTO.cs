using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string CommentName { get; set; }
        public string CommentTitle { get; set; }
        public string CommentImage { get; set; }
        public string CommentDetail { get; set; }
    }
}

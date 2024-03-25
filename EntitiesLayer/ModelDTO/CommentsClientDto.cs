using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class CommentsClientDto :CommentDTO
    {
        public string TrLangueDetail { get; set; }
        public string EnLangueDetail { get; set; }
    }
}

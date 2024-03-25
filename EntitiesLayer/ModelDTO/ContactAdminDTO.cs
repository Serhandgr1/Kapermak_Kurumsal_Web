using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class ContactAdminDTO
    {
        public int Id { get; set; }
        public string AdminMail { get; set; }
        public string AdminPhoneNumber { get; set; }
        public string AdminAddress { get; set; }
    }
}

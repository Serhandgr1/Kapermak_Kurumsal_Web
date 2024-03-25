using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class ContactAdminEntities
    {
        [Key]
        public int Id { get; set; } 
        public string AdminMail { get; set; }
        public string AdminPhoneNumber { get; set; }
        public string AdminAddress { get; set; }
    }
}

using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IContactService
    {
        Task Create(ContactDTO entites);
        Task Delete(int Id);
        Task<List<ContactDTO>> GetAllData();
        Task Update(ContactDTO entites);
        Task<ContactDTO> GetById(int Id);
        Task<string> SendMail(ContactDTO contactDTO);
    }
}

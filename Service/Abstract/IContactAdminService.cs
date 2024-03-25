using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IContactAdminService
    {
        Task Create(ContactAdminDTO entites);
        Task Delete(int Id);
        Task<List<ContactAdminDTO>> GetAllData();
        Task Update(ContactAdminDTO entites);
        Task<ContactAdminDTO> GetById(int Id);
    }
}

using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IUserService
    {
        Task Create(UserDTO entites);
        Task Delete(int Id);
        Task<List<UserDTO>> GetAllData();
        Task Update(UserDTO entites);
        Task<UserDTO> GetById(int Id);
    }
}

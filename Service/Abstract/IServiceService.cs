using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IServiceService
    {
        Task Create(ServicesDTO entites);
        Task Delete(int Id);
        Task<List<ServicesDTO>> GetAllData();
        Task Update(ServicesDTO entites);
        Task<ServicesDTO> GetById(int Id);
    }
}

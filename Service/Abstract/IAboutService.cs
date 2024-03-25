using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IAboutService
    {

        Task Create(AboutDTO entites);
        Task Delete(int Id);
        Task<List<AboutDTO>> GetAllData();
        Task Update(AboutDTO entites);
        Task<AboutDTO> GetById(int Id);
    }
}

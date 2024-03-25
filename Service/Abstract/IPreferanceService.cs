using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IPreferanceService
    {
        Task Create(PreferenceDTO entites);
        Task Delete(int Id);
        Task<List<PreferenceDTO>> GetAllData();
        Task Update(PreferenceDTO entites);
        Task<PreferenceDTO> GetById(int Id);
    }
}

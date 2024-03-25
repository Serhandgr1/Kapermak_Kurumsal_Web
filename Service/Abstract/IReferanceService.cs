using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IReferanceService
    {
        Task Create(ReferangeDTO entites);
        Task Delete(int Id);
        Task<List<ReferangeDTO>> GetAllData();
        Task Update(ReferangeDTO entites);
        Task<ReferangeDTO> GetById(int Id);
    }
}

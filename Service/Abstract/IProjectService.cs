using EntitiesLayer;
using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProjectService
    {
        Task Create(ProjectDTO entites);
        Task Delete(int Id);
        Task<List<ProjectDTO>> GetAllData();
        Task Update(ProjectDTO entites);
        Task<ProjectDTO> GetById(int Id);
        Task<IEnumerable<ProjectDTO>> GetAllProjectsPagination(RequestParameters parameters);
    }
}

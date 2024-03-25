using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICategoryService
    {
        Task Create(CategoryDTO entites);
        Task Delete(int Id);
        Task<List<CategoryDTO>> GetAllData();
        Task Update(CategoryDTO entites);
        Task<CategoryDTO> GetById(int Id);
        Task<string> GetCategoryByProduct(int id);
    }
}

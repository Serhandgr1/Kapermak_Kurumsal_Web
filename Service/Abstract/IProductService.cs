using EntitiesLayer;
using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProductService
    {
        Task Create(ProductDTO entites);
        Task Delete(int Id);
        Task<List<ProductDTO>> GetAllData();
        Task Update(ProductDTO entites);
        Task<ProductDTO> GetById(int Id);
       Task<IEnumerable<ProductDTO>> GetAllProductsPagination(RequestParameters parameters, bool trackChanges , int? category, string? search);
    }
}

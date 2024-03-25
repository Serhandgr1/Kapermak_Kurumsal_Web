using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepostoryProducts
    {
        Task<IEnumerable<ProductEntities>> GetPagedProducts(RequestParameters parameters, bool trackChanges,int? category , string? search);
     
    }
}

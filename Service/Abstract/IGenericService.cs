using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task Create(T entites);
        Task Delete(int Id);
        Task<List<T>> GetAllData();
        Task Update(T entites);
        Task<T> GetById(int Id);
    }
}

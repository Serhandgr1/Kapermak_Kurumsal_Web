using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepostory<T> where T : class
    {
        Task Create(T entites);
        Task Delete(T entites);
        Task<List<T>> GetAllData();
        Task Update(T entites);
        Task<T> GetById(int Id);
    }
}

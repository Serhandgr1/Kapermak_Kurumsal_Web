using DataAccessLayer.Abstract;
using EntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RepostiryCategory : IRepostoryCategory
    {
        private readonly DbContextOptions _options;
        public RepostiryCategory(DbContextOptions options)
        {
            _options = options;
        }
        public async Task<string> GetCategoryByProduct(int id)
        {
            using (var db = new DataContext(_options))
            {
                var  data = await db.Set<ProductEntities>().AsQueryable().Where(x => x.CategoryId == id).ToListAsync();
                if (data.Count > 0)
                {
                    return "true";
                }
                else { return "false"; }
            }
        }
    }
}

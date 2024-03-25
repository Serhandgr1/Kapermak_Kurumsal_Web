using EntitiesLayer.Entities;
using EntitiesLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using EntitiesLayer.ModelDTO;

namespace DataAccessLayer.Concrete
{
    public class RepostoryProject : IRepostoryProjects
    {
        private readonly DbContextOptions _options;
        public RepostoryProject(DbContextOptions options)
        {
            _options = options;
        }
        public async Task<IEnumerable<ProjectEntities>> GetPagedProducts(RequestParameters parameters)
        {
            using (var db = new DataContext(_options))
            {
                    var veriler = db.Set<ProjectEntities>().AsQueryable().Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                   .Take(parameters.PageSize)
                                   .ToList();
                    return veriler;
            }

        } 
       
    }
}

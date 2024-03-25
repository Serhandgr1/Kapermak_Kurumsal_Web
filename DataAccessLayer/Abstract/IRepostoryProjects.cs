using EntitiesLayer.Entities;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepostoryProjects
    {
        Task<IEnumerable<ProjectEntities>> GetPagedProducts(RequestParameters parameters);
     
    }
}

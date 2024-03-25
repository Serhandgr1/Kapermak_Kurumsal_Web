using DataAccessLayer.Abstract;
using EntitiesLayer;
using EntitiesLayer.Entities;
using EntitiesLayer.ModelDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RepostoryProduct:IRepostoryProducts
    {
        private readonly DbContextOptions _options;
        public RepostoryProduct(DbContextOptions options)
        {
            _options = options;
        }
        public async Task<IEnumerable<ProductEntities>> GetPagedProducts(RequestParameters parameters, bool trackChanges,int? category , string? search)
        {
            using (var db = new DataContext(_options))
            {

                if (category != 0 && category != null)
                {
                    var veriler = db.Set<ProductEntities>().AsQueryable().Where(x => x.CategoryId == category).Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                 .Take(parameters.PageSize)
                                 .ToList();
                    return veriler;
                }
                else if (search != "" && search != null)
                {
                    var veriler = db.Set<ProductEntities>().AsQueryable().Where(x => x.ProductTitle.Contains(search) || x.ProductDetail.Contains(search)||x.ProductSpacial.Contains(search)).Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                  .Take(parameters.PageSize)
                                  .ToList();
                    return veriler;
                }
                else 
                {
                    var veriler = db.Set<ProductEntities>().AsQueryable().Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                   .Take(parameters.PageSize)
                                   .ToList();
                    return veriler;
                }
              
               
            }
            
        }
     
    }
}

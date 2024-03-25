using AutoMapper;
using DataAccessLayer.Abstract;
using EntitiesLayer;
using EntitiesLayer.Entities;
using EntitiesLayer.ModelDTO;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
	public class ProductService : IProductService
	{
        private readonly IGenericRepostory<ProductEntities> _genericRepostoryProduct;
        private readonly IRepostoryProducts _repostoryProducts;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepostory<ProductEntities> genericRepostoryProduct, IRepostoryProducts repostoryProducts, IMapper mapper)
        {
            _genericRepostoryProduct = genericRepostoryProduct;
            _repostoryProducts = repostoryProducts;
            _mapper = mapper;
        }
        public async Task Create(ProductDTO entites)
        {
            var entity = _mapper.Map<ProductEntities>(entites);
            await _genericRepostoryProduct.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<ProductEntities>(deleteDto);
            await _genericRepostoryProduct.Delete(entity);
        }

        public async Task<List<ProductDTO>> GetAllData()
        {
            var entity = await _genericRepostoryProduct.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<ProductDTO>>(entity);
            return Dto;
        }
  
        public async Task<ProductDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryProduct.GetById(Id);
            var dto = _mapper.Map<ProductDTO>(entity);
            return dto;
        }

        public async Task Update(ProductDTO entites)
        {
            var entity = _mapper.Map<ProductEntities>(entites);
            await _genericRepostoryProduct.Update(entity);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsPagination(RequestParameters parameters, bool trackChanges, int? category , string? search)
        {

            var query = await _repostoryProducts.GetPagedProducts(parameters, trackChanges , category , search);
            var dto =  _mapper.Map<List<ProductDTO>>(query);
            return dto;
        }
    }
}

using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
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
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepostory<CategoryEntities> _genericRepostoryCategory;
        private readonly IRepostoryCategory _repostroyCategory;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepostory<CategoryEntities> genericRepostoryCategory, IRepostoryCategory repostroyCategory , IMapper mapper)
        {
            _mapper = mapper;
            _genericRepostoryCategory = genericRepostoryCategory;
            _repostroyCategory = repostroyCategory;
        }
        public async Task Create(CategoryDTO dto)
        {
          var data = _mapper.Map<CategoryEntities>(dto);
           await _genericRepostoryCategory.Create(data);
           
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<CategoryEntities>(deleteDto);
            await _genericRepostoryCategory.Delete(entity);
        }

        public async Task<List<CategoryDTO>> GetAllData()
        {
            var entity = await _genericRepostoryCategory.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<CategoryDTO>>(entity);
            return Dto;
        }
        public async Task<string> GetCategoryByProduct(int id)
        {
            var data = await _repostroyCategory.GetCategoryByProduct(id);
            return data;
        }

        public async Task<CategoryDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryCategory.GetById(Id);
            var dto = _mapper.Map<CategoryDTO>(entity);
            return dto;
        }

        public async Task Update(CategoryDTO entites)
        {
            var entity = _mapper.Map<CategoryEntities>(entites);
            await _genericRepostoryCategory.Update(entity);
        }
    }
}

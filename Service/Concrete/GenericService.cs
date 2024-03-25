using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GenericService<T>:IGenericService<T> where T: class
    {
        private readonly IGenericRepostory<UserEntites> _genericRepostoryUser;
        private readonly IMapper _mapper;
        public GenericService(IMapper mapper , IGenericRepostory<UserEntites> genericRepostoryUser)
        {
            _mapper = mapper;
            _genericRepostoryUser= genericRepostoryUser;
        }

        public async Task Create(T entites)
        {
            var entity = _mapper.Map<UserEntites>(entites);
            await _genericRepostoryUser.Create(entity);
        }
      

        public async Task Delete(int Id)
        {
            var dto = await GetById(Id);
            var entity = _mapper.Map<UserEntites>(dto);
            await _genericRepostoryUser.Delete(entity);
        }

        public async Task<List<T>> GetAllData()
        {
          var entity = await  _genericRepostoryUser.GetAllData();
          var dto =_mapper.Map<List<UserEntites>, List<T>>(entity);
          return dto;
        }

        public  async Task<T> GetById(int Id)
        {
            var data = await _genericRepostoryUser.GetById(Id); 
            var dto = _mapper.Map<T>(data); 
            return dto;
        }

        public async Task Update(T entities)
        {
            var entity=_mapper.Map<UserEntites>(entities);
            await _genericRepostoryUser.Update(entity);
        }
      
    }
}

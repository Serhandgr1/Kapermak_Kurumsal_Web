using AutoMapper;
using DataAccessLayer.Abstract;
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
    public class UserService:IUserService
    {
        private readonly IGenericRepostory<UserEntites> _genericRepostoryUser;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IGenericRepostory<UserEntites> genericRepostoryUser)
        {
            _mapper = mapper;
            _genericRepostoryUser = genericRepostoryUser;
        }

        public async Task Create(UserDTO entites)
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

        public async Task<List<UserDTO>> GetAllData()
        {
            var entity = await _genericRepostoryUser.GetAllData();
            var dto = _mapper.Map<List<UserEntites>, List<UserDTO>>(entity);
            return dto;
        }

        public async Task<UserDTO> GetById(int Id)
        {
            var data = await _genericRepostoryUser.GetById(Id);
            var dto = _mapper.Map<UserDTO>(data);
            return dto;
        }

        public async Task Update(UserDTO entities)
        {
            var entity = _mapper.Map<UserEntites>(entities);
            await _genericRepostoryUser.Update(entity);
        }

    

    }
}

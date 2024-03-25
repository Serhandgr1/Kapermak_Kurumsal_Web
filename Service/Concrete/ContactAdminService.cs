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
    public class ContactAdminService : IContactAdminService
    {
        private readonly IGenericRepostory<ContactAdminEntities> _genericRepostoryContactAdmin;
        private readonly IMapper _mapper;
        public ContactAdminService(IGenericRepostory<ContactAdminEntities> genericRepostoryContactAdmin, IMapper mapper)
        {
            _genericRepostoryContactAdmin = genericRepostoryContactAdmin;
            _mapper = mapper;
        }
        public async Task Create(ContactAdminDTO entites)
        {

            var entity = _mapper.Map<ContactAdminEntities>(entites);
            await _genericRepostoryContactAdmin.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<ContactAdminEntities>(deleteDto);
            await _genericRepostoryContactAdmin.Delete(entity);
        }

        public async Task<List<ContactAdminDTO>> GetAllData()
        {
            var entity = await _genericRepostoryContactAdmin.GetAllData();
            var Dto = _mapper.Map<List<ContactAdminDTO>>(entity);
            return Dto;
        }

        public async Task<ContactAdminDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryContactAdmin.GetById(Id);
            var dto = _mapper.Map<ContactAdminDTO>(entity);
            return dto;
        }

        public async Task Update(ContactAdminDTO entites)
        {
            var entity = _mapper.Map<ContactAdminEntities>(entites);
            await _genericRepostoryContactAdmin.Update(entity);
        }
    }
}

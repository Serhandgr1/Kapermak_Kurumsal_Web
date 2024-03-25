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
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepostory<ServicesEntities> _genericRepostoryService;
        private readonly IMapper _mapper;
        public ServiceService(IGenericRepostory<ServicesEntities> genericRepostoryService, IMapper mapper)
        {
            _genericRepostoryService = genericRepostoryService;
            _mapper = mapper;
        }
        public async Task Create(ServicesDTO entites)
        {
            var entity = _mapper.Map<ServicesEntities>(entites);
            await _genericRepostoryService.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<ServicesEntities>(deleteDto);
            await _genericRepostoryService.Delete(entity);
        }

        public async Task<List<ServicesDTO>> GetAllData()
        {
            var entity = await _genericRepostoryService.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<ServicesDTO>>(entity);
            return Dto;
        }

        public async Task<ServicesDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryService.GetById(Id);
            var dto = _mapper.Map<ServicesDTO>(entity);
            return dto;
        }

        public async Task Update(ServicesDTO entites)
        {
            var entity = _mapper.Map<ServicesEntities>(entites);
            await _genericRepostoryService.Update(entity);
        }
    }
}

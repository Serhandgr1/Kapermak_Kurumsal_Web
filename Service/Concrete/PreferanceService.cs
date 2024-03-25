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
    public class PreferanceService : IPreferanceService
    {
        private readonly IGenericRepostory<PreferenceEntiites> _genericRepostoryPreferance;
        private readonly IMapper _mapper;
        public PreferanceService(IGenericRepostory<PreferenceEntiites> genericRepostoryPreferance, IMapper mapper)
        {
            _genericRepostoryPreferance = genericRepostoryPreferance;
            _mapper = mapper;
        }
        public async Task Create(PreferenceDTO entites)
        {
            var entity = _mapper.Map<PreferenceEntiites>(entites);
            await _genericRepostoryPreferance.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<PreferenceEntiites>(deleteDto);
            await _genericRepostoryPreferance.Delete(entity);
        }

        public async Task<List<PreferenceDTO>> GetAllData()
        {
            var entity = await _genericRepostoryPreferance.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<PreferenceDTO>>(entity);
            return Dto;
        }

        public async Task<PreferenceDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryPreferance.GetById(Id);
            var dto = _mapper.Map<PreferenceDTO>(entity);
            return dto;
        }

        public async Task Update(PreferenceDTO entites)
        {
            var entity = _mapper.Map<PreferenceEntiites>(entites);
            await _genericRepostoryPreferance.Update(entity);
        }
    }
}

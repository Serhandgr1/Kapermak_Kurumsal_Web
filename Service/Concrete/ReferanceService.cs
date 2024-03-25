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
	public class ReferanceService : IReferanceService
	{
		private readonly IGenericRepostory<ReferanceEntities> _genericRepostory;
		private readonly IMapper _mapper;
		public ReferanceService(IGenericRepostory<ReferanceEntities> genericRepostory, IMapper mapper)
		{
			_genericRepostory = genericRepostory;
			_mapper = mapper;
		}

		public async Task Create(ReferangeDTO entites)
		{
			var entity = _mapper.Map<ReferanceEntities>(entites);
			await _genericRepostory.Create(entity);
		}

		public async Task Delete(int Id)
		{
			var dto = await GetById(Id);
			var entity = _mapper.Map<ReferanceEntities>(dto);
			await _genericRepostory.Delete(entity);
		}

		public async Task<List<ReferangeDTO>> GetAllData()
		{
			var data = await _genericRepostory.GetAllData();
			var dto = _mapper.Map<List<ReferangeDTO>>(data);
			return dto;
		}

		public async Task<ReferangeDTO> GetById(int Id)
		{
			var entity = await _genericRepostory.GetById(Id);
			var dto = _mapper.Map<ReferangeDTO>(entity);
			return dto;
		}

		public async Task Update(ReferangeDTO entites)
		{
			var entity = _mapper.Map<ReferanceEntities>(entites);
			await _genericRepostory.Update(entity);
		}
	}
}

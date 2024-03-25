using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
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
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepostory<ProjectEntities> _genericRepostoryProject;
        private readonly IRepostoryProjects _repostoryProject;
        private readonly IMapper _mapper;
        public ProjectService(IGenericRepostory<ProjectEntities> genericRepostoryProject, IRepostoryProjects repostoryProject , IMapper mapper)
        {
            _genericRepostoryProject = genericRepostoryProject;
            _repostoryProject=repostoryProject;
            _mapper = mapper;
        }
        public async Task Create(ProjectDTO entites)
        {
            var entity = _mapper.Map<ProjectEntities>(entites);
            await _genericRepostoryProject.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<ProjectEntities>(deleteDto);
            await _genericRepostoryProject.Delete(entity);
        }

        public async Task<List<ProjectDTO>> GetAllData()
        {
            var entity = await _genericRepostoryProject.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<ProjectDTO>>(entity);
            return Dto;
        }
        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsPagination(RequestParameters parameters)
        {
            var query = await _repostoryProject.GetPagedProducts(parameters);
            var dto = _mapper.Map<List<ProjectDTO>>(query);
            return dto;
        }
        public async Task<ProjectDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryProject.GetById(Id);
            var dto = _mapper.Map<ProjectDTO>(entity);
            return dto;
        }

        public async Task Update(ProjectDTO entites)
        {
            var entity = _mapper.Map<ProjectEntities>(entites);
            await _genericRepostoryProject.Update(entity);
        }
    }
}

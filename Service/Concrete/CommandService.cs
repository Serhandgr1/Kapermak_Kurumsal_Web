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
    public class CommandService : ICommandService
    {
        private readonly IGenericRepostory<CommentEntities> _genericRepostoryComment;
        private readonly IMapper _mapper;
        public CommandService(IGenericRepostory<CommentEntities> genericRepostoryCommant, IMapper mapper)
        {
            _genericRepostoryComment = genericRepostoryCommant;
            _mapper = mapper;
        }
        public async Task Create(CommentDTO entites)
        {
            var entity = _mapper.Map<CommentEntities>(entites);
            await _genericRepostoryComment.Create(entity);
        }

        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<CommentEntities>(deleteDto);
            await _genericRepostoryComment.Delete(entity);
        }

        public async Task<List<CommentDTO>> GetAllData()
        {
            var entity = await _genericRepostoryComment.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto = _mapper.Map<List<CommentDTO>>(entity);
            return Dto;
        }

        public async Task<CommentDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryComment.GetById(Id);
            var dto = _mapper.Map<CommentDTO>(entity);
            return dto;
        }

        public async Task Update(CommentDTO entites)
        {
            var entity = _mapper.Map<CommentEntities>(entites);
            await _genericRepostoryComment.Update(entity);
        }
    }
}

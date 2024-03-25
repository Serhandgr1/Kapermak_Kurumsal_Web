using AutoMapper;
using DataAccessLayer.Abstract;
using EntitiesLayer.Entities;
using EntitiesLayer.ModelDTO;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IGenericRepostory<AboutEntites> _genericRepostoryAbout;
        private readonly IMapper _mapper;
       
        public AboutService(IGenericRepostory<AboutEntites> genericRepostoryAbout, IMapper mapper)
        {
            _genericRepostoryAbout = genericRepostoryAbout;
           
            _mapper = mapper;
        }
        public async Task Create(AboutDTO entites)
        {
           var entity= _mapper.Map<AboutEntites>(entites);
           await _genericRepostoryAbout.Create(entity);    
        }

        public async Task Delete(int Id)
        {
            var deleteDto= await GetById(Id);
            var entity=_mapper.Map<AboutEntites>(deleteDto);
            await _genericRepostoryAbout.Delete(entity);
        }

        public async Task<List<AboutDTO>> GetAllData()
        {
           var entity =  await _genericRepostoryAbout.GetAllData();
            //KT  _mapper.Map<List<AboutEntites>, List<AboutDTO>>(entity);
            var Dto =  _mapper.Map<List<AboutDTO>>(entity);
           return Dto;
        }

        public async Task<AboutDTO> GetById(int Id)
        {
            var entity= await _genericRepostoryAbout.GetById(Id);
            var dto =  _mapper.Map<AboutDTO>(entity);
            return dto;
        }

        public async Task Update(AboutDTO entites)
        {
          
            var entity =  _mapper.Map<AboutEntites>(entites);
           await _genericRepostoryAbout.Update(entity);
        }

    }
}

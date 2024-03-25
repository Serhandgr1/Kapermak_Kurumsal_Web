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
	public class ContactService : IContactService
	{
        private readonly IGenericRepostory<ContactEntities> _genericRepostoryContact;
        private readonly IGenericRepostory<ContactAdminEntities> _genericRepostoryContactAdmin;
        private readonly IMapper _mapper;
        public ContactService(IGenericRepostory<ContactEntities> genericRepostoryContact, IGenericRepostory<ContactAdminEntities> contactAdmin, IMapper mapper)
        {
            _genericRepostoryContact = genericRepostoryContact;
            _genericRepostoryContactAdmin = contactAdmin;
            _mapper = mapper;
        }
        public async Task Create(ContactDTO entites)
        {
            var entity = _mapper.Map<ContactEntities>(entites);
            await _genericRepostoryContact.Create(entity);
        }

        public async Task<string> SendMail(ContactDTO contactDTO) 
        {
            try {
                string AdminMail = "";
                string Message = $"{contactDTO.Name}{contactDTO.Message}{contactDTO.Email}{contactDTO.PhoneNumber}";
                var data = await _genericRepostoryContactAdmin.GetAllData();
                MailSender mailSender = new MailSender();
                foreach (var item in data) 
                {
                    AdminMail=item.AdminMail;
                    mailSender.SendMail(AdminMail, contactDTO.Name, Message);
                    var entity = _mapper.Map<ContactEntities>(contactDTO);
                    await _genericRepostoryContact.Create(entity);
                }
               
               
                return "Mesaj Başarı ile iletildi";
            }
            catch (Exception ex) { 
                return ex.Message;
            }
          
        }
        public async Task Delete(int Id)
        {
            var deleteDto = await GetById(Id);
            var entity = _mapper.Map<ContactEntities>(deleteDto);
            await _genericRepostoryContact.Delete(entity);
        }

        public async Task<List<ContactDTO>> GetAllData()
        {
            var entity = await _genericRepostoryContact.GetAllData();
            var Dto = _mapper.Map<List<ContactDTO>>(entity);
            return Dto;
        }

        public async Task<ContactDTO> GetById(int Id)
        {
            var entity = await _genericRepostoryContact.GetById(Id);
            var dto = _mapper.Map<ContactDTO>(entity);
            return dto;
        }

        public async Task Update(ContactDTO entites)
        {
            var entity = _mapper.Map<ContactEntities>(entites);
            await _genericRepostoryContact.Update(entity);
        }
    }
}

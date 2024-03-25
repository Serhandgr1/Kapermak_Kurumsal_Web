using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactApiController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet("get-all-contact")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllContact() 
        {
            var data= await  _contactService.GetAllData();
            return Ok(data);
        }

        [HttpPost("postmail")]
        public async Task<IActionResult> PostMail(ContactDTO contact) 
        {
                //var data = await  _contactService.SendMail(contact);
            await _contactService.Create(contact);
            return Ok();
        }
        [HttpDelete("delete-contact")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllContact(int id)
        {
            await _contactService.Delete(id);
            return Ok();
        }
    }
}

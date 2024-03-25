using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.Concrete;
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
    public class ContactAdminController : ControllerBase
    {
        private readonly IContactAdminService _contactAdminService;
        public ContactAdminController(IContactAdminService contactAdminService)
        {
             _contactAdminService = contactAdminService;
        }
        [HttpGet("get-all-contact-admin")]
        public async Task<IActionResult> GetAllContactAdmin()
        {
            var allContactAdmin = await _contactAdminService.GetAllData();
            return Ok(allContactAdmin);
        }
        [HttpGet("get-by-id-contact-admin")]
        public async Task<IActionResult> GetByIdContactAdmin(int Id)
        {
            var data = await _contactAdminService.GetById(Id);
            return Ok(data);
        }
        [HttpPost("create-admin-contact")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdminContact(ContactAdminDTO contactAdmin)
        {
            await _contactAdminService.Create(contactAdmin);
            return Ok();
        }
        [HttpPut("update-contact-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateContactAdmin(ContactAdminDTO contactAdmin)
        {
            await _contactAdminService.Update(contactAdmin);
            return Ok(contactAdmin);
        }
        [HttpDelete("delete-contact-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContactAdmin(int Id)
        {
            await _contactAdminService.Delete(Id);
            return Ok();
        }
    }
}

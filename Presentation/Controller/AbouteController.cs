using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbouteController : ControllerBase
    {

        private readonly IAboutService _aboutService;
        public AbouteController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("get-all-about")]
        public async  Task<IActionResult> GetAllAboute() 
        {
            
            var allAboute = await _aboutService.GetAllData();
            return Ok(allAboute);
        }
        [HttpGet("get-by-id-about")]
        public async Task<IActionResult> GetByIdAboute(int Id) 
        {
           var data =  await _aboutService.GetById(Id);
           return Ok(data); 
        }
        [HttpPost("post-aboute")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAboute(AboutDTO aboutDTO) 
        {
           await  _aboutService.Create(aboutDTO);  
           return Ok();
        }
        [HttpPut("update-aboute")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAbout(AboutDTO aboutDTO) 
        {
          await  _aboutService.Update(aboutDTO);     
            return Ok(aboutDTO);
        }
        [HttpDelete("delete-aboute")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAbote(int Id) 
        {
            await _aboutService.Delete(Id);
            return Ok();
        }

    }
}

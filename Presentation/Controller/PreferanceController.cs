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
    public class PreferanceController : ControllerBase
    {
        private readonly IPreferanceService _preferanceService;
        public PreferanceController(IPreferanceService preferanceService)
        {
            _preferanceService = preferanceService;
        }
        [HttpGet("get-all-preferance")]
        public async Task<IActionResult> GetAllPreferance()
        {
            

            var data = await _preferanceService.GetAllData();

            return Ok(data);
        }
        [HttpGet("get-preferance-by-id")]
        public async Task<IActionResult> GetPreferanceById(int id)
        {
            var data = await _preferanceService.GetById(id);
            return Ok(data);
        }
        [HttpPost("post-preferance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostPreferance(PreferenceDTO preferenceDTO) 
        {
             await _preferanceService.Create(preferenceDTO);  
             return Ok(preferenceDTO);   
        }
        [HttpPut("update-preferance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePreferance(PreferenceDTO preferenceDTO) 
        {
             await  _preferanceService.Update(preferenceDTO);
                return Ok();
        }
        [HttpDelete("delete-preferance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePreferance(int id) 
        {   
            await  _preferanceService.Delete(id);
            return Ok();
        }
    }
}

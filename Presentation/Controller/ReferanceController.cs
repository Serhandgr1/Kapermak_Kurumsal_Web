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
    public class ReferanceController : ControllerBase
    {
        private readonly IReferanceService _referanceService;

        public ReferanceController(IReferanceService referanceService)
        {
            _referanceService = referanceService;
        }

        [HttpGet("get-all-referance")]
        public async Task<IActionResult> GetAllReferance() 
        {


            var data = await _referanceService.GetAllData();

            return Ok(data);
        }
        [HttpGet("get-referance-by-id")]
        public async Task<IActionResult> GetReferanceById(int id) 
        {
            var data =  await  _referanceService.GetById(id);
            return Ok(data);
        }
        [HttpPost("post-referance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostReferance(ReferangeDTO referangeDTO) 
        {
            await _referanceService.Create(referangeDTO);
            return Ok();
        }
        [HttpPut("update-referance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateReferance(ReferangeDTO referangeDTO)
        {  
              await _referanceService.Update(referangeDTO);
              return Ok();
        }
        [HttpDelete("delete-referance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReferance(int id) 
        {
            await  _referanceService.Delete(id);
            return Ok();
        }

    }
}

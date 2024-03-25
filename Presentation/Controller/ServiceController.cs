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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("get-all-service")]
        public async Task<IActionResult> GetAllService()
        {

            var data = await _serviceService.GetAllData();
            return Ok(data);
        }
        [HttpGet("get-service-by-id")]
        public async Task<IActionResult> GetServiceById(int id) 
        {
            var data= await _serviceService.GetById(id);
            return Ok(data);
        }
        [HttpPost("post-service")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostService(ServicesDTO servicesDTO) 
        {
            await _serviceService.Create(servicesDTO);
            return Ok(servicesDTO);
        }
        [HttpPut("update-service")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService(ServicesDTO servicesDTO) 
        {
           await _serviceService.Update(servicesDTO);
            return Ok(servicesDTO); 
        }
        [HttpDelete("delete-service")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int id) 
        {
            await _serviceService.Delete(id);
            return Ok();
        }
    }
}

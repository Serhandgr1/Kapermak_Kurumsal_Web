using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommendController : ControllerBase
    {

        private readonly ICommandService _commandService;
        public CommendController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet("get-all-commend")]
        public async Task<IActionResult> GetAllCommand()
        {
            
            var data = await _commandService.GetAllData();

            return Ok(data);
        }
        [HttpGet("get-commend-by-id")]
        public async Task<IActionResult> GetCommandById(int id)
        {
            var data = await _commandService.GetById(id);
            return Ok(data);
        }
        [HttpPost("post-commend")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCommand(CommentDTO commentDTO)
        {

            await _commandService.Create(commentDTO);
            return Ok(commentDTO);
        }
        [HttpPut("update-commend")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCommend(CommentDTO commentDTO) 
        {
           await _commandService.Update(commentDTO);
            return Ok(commentDTO);
        }
        [HttpDelete("delete-commend")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCommend(int id) 
        {
             await   _commandService.Delete(id);
            return Ok();  
        }
    
    }
}

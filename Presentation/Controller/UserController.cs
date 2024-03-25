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
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _userService.GetAllData();
            return Ok(data);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var data = await _userService.GetById(Id);
            return Ok(data);
        }
        [HttpPost("post-user")]
        public async Task<IActionResult> PostUser(UserDTO userDTO) 
        {
            await _userService.Create(userDTO);
            return Ok(userDTO);
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserDTO userDTO) 
        {
           await _userService.Update(userDTO);
            return Ok(userDTO);
        }
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            await _userService.Delete(Id);
            return Ok();
        }
    }
}

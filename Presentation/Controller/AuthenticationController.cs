using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _serviceManager;
        public AuthenticationController(IAuthenticationService serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO user)
        {
            var result = await _serviceManager.RegisterUser(user);

            if (result.Succeeded) return Ok(result);
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (!await _serviceManager.ValidateUser(user))
                return Unauthorized(); //401

            var tokenDto = await _serviceManager.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }
        [HttpPost("refresh")]

        public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDto)
        {
            var tokenDtoToReturn = await _serviceManager.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }

    }
}

using EntitiesLayer;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("get-all-project")]
        public async Task<IActionResult> GetAllProject() 
        {
            var data = await _projectService.GetAllData();
            return Ok(data);
        }
        [HttpGet("page-project-list")]
        public async Task<IActionResult> GetPageProducts([FromQuery] RequestParameters requestParameters)
        {
            var data = await _projectService.GetAllProjectsPagination(requestParameters);
            return Ok(data);
        }
        [HttpGet("get-project-by-id")]
        public async Task<IActionResult> GetProjectById(int id) 
        {
            var data =  await _projectService.GetById(id);
            return Ok(data);
        }
        [HttpPost("post-project")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProject(ProjectDTO projectDTO) 
        {
            await  _projectService.Create(projectDTO);
            return Ok(projectDTO);
        }
        [HttpPut("update-project")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProject(ProjectDTO projectDTO) 
        {
            await  _projectService.Update(projectDTO);
            return Ok(projectDTO);
        }
        [HttpDelete("delete-project")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id) 
        {
                await _projectService.Delete(id);
                return Ok();    
        }

   }
}

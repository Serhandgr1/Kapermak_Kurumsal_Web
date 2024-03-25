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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory()
        {

            var allAboute = await _categoryService.GetAllData();
            foreach (var category in allAboute) 
            {
                var deger = await _categoryService.GetCategoryByProduct(category.Id);
                if (deger != null && deger == "true")
                {
                    category.CategoryProduct = true;
                }
                else
                {
                    category.CategoryProduct = false;
                }
            }
            return Ok(allAboute);
        }
        [HttpGet("get-category-by-product")]
        public async Task<string> GetCategoryByProduct(int id )
        {
            var deger = await _categoryService.GetCategoryByProduct(id);
            if (deger != null && deger == "true")
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        [HttpGet("get-by-id-category")]
        public async Task<IActionResult> GetByIdCategory(int Id)
        {
            var data = await _categoryService.GetById(Id);
            return Ok(data);
        }
        [HttpPost("post-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCategory(CategoryDTO category)
        {
            await _categoryService.Create(category);
            return Ok();
        }
        [HttpPut("update-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO category)
        {
            await _categoryService.Update(category);
            return Ok(category);
        }
        [HttpDelete("delete-category")]
        [Authorize(Roles = "Admin")]
        public async Task<string> DeleteCategory(int Id)
        {
            var deger = await _categoryService.GetCategoryByProduct(Id);
            if (deger != null && deger == "true")
            {
                return "false";
            }
            else
            {
                await _categoryService.Delete(Id);
                return "true";
            }
        }
    }
}

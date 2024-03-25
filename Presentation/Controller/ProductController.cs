using EntitiesLayer;
using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAllProduct() 
        {

            var data = await _productService.GetAllData();
            return Ok(data);    
        }
       

        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById(int id) 
        {
                 var data= await  _productService.GetById(id);
                 return Ok(data);
        }
        [HttpGet("page-product-list")]
        public async Task<IActionResult> GetPageProducts([FromQuery]RequestParameters requestParameters, int? category , string? search)
        {
            //[FromQuery] RequestParameters requestParameters
            var data = await _productService.GetAllProductsPagination(requestParameters, false , category, search);
            return Ok(data);    
        }
        
        [HttpPost("add-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct(ProductDTO productDTO) 
        {
            try {
                await _productService.Create(productDTO);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("update-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO) 
        {
                 await  _productService.Update(productDTO);
                 return Ok(productDTO);
        }
        [HttpDelete("delete-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            try {

                await _productService.Delete(id);
                return Ok();
            }
            catch(Exception) 
            { 
                return BadRequest(); 
            }
           
        }
      
    }
}

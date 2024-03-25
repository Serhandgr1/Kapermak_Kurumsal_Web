using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Controllers;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        GenericRequests<CategoryDTO> genericRequests = new GenericRequests<CategoryDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public CategoryController(IDistributedCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public IActionResult Index(string? posts)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = posts;
                return View("CategoryIndex");
            }else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllCategory(string? updated)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    List<CategoryClientDto> categoryClientDtos = new List<CategoryClientDto>();
                    ViewBag.Message = updated;
                    var data = await genericRequests.GetHttpRequest("api/Categories/get-all-category");
                    foreach (var item in data)
                    {
                        var clientCategoy = _mapper.Map<CategoryClientDto>(item);
                        clientCategoy.TrLangue = await addJsonLanguage.GetValue(clientCategoy.CategoryName, "tr-TR");
                        clientCategoy.EnLangue = await addJsonLanguage.GetValue(clientCategoy.CategoryName, "en-US");
                        categoryClientDtos.Add(clientCategoy);
                    }
                    return View("GetCategoryIndex", categoryClientDtos);
                }
                else return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Login"); }
            
        }
        public async Task<IActionResult> GetUpdateCategoryIndex(CategoryClientDto categoryClientDto)
        {
            return View("UpdateCategoryIndex", categoryClientDto);
        }
        public async Task<IActionResult> UpdateCategory(CategoryClientDto categoryClientDto)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var category = genericRequests.GetByIdGeneric("api/Categories/get-by-id-category", categoryClientDto.Id).Result;
                    await addJsonLanguage.UpdateLangue(category.CategoryName, categoryClientDto.TrLangue, categoryClientDto.EnLangue);
                    var categoryDTO = _mapper.Map<CategoryDTO>(categoryClientDto);
                    var deger = await genericRequests.UpdateRequestGeneric("api/Categories/update-category", categoryDTO);
                    return RedirectToAction("GetAllCategory", "Category", new { updated = deger });
                }
                else { return RedirectToAction("Index", "Login"); }
                }catch { return RedirectToAction("GetAllCategory", "Category", new { updated = "Başarısız" }); }
            
        }
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDTO category)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(category.CategoryName, category.CategoryName);
                    await addJsonLanguage.EnSetLanguage(category.CategoryName, category.CategoryName);
                    string deger = await genericRequests.PostRequestGeneric("api/Categories/post-category", category);
                    // post category
                    return RedirectToAction("Index", "Category", new { posts = deger });
                }
                else { return RedirectToAction("Index", "Login"); }

            }
            catch { return RedirectToAction("Index", "Category", new { posts = "Başarısız" }); }
            
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var category = genericRequests.GetByIdGeneric("api/Categories/get-by-id-category", id);
                    await addJsonLanguage.DeleteLanguage(category.Result.CategoryName);
                    string deger = await deleteRequest.DeleteRequestGeneric("api/Categories/delete-category", id);
                    if (deger == "Başarısız")
                    {
                        return RedirectToAction("GetAllCategory", "Category", new { updated = "true" });
                    }
                    else
                    {// id li categoriyi sil
                        return RedirectToAction("GetAllCategory", "Category", new { updated = "Başarılı" });
                    }
                }
                else { return RedirectToAction("Index", "Login"); }

            }
            catch { return RedirectToAction("GetAllCategory", "Category", new { updated = "Başarılı" }); }
            
        }
    }
}

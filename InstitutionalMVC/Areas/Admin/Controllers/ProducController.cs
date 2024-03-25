using AutoMapper;
using EntitiesLayer.ModelDTO;
using Humanizer;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.IdentityModel.Protocols.WSFederation.WSFederationConstants;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
 
    public class ProducController : Controller
    {
        private readonly IMapper _mapper;
        private static int screenSize;
        private static string deger;
        private static int categorys;
        private static int pageNumber = 1;
        private static TokenDTO tokenDTO;
        FileManagerAsycn FileManager = new FileManagerAsycn();
        GenericRequests<ProductDTO> genericRequests= new GenericRequests<ProductDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        GetPaginationProduct paginationProduct  = new GetPaginationProduct();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDistributedCache _cache;
        public ProducController(IMapper mapper , IWebHostEnvironment webHostEnvironment, IDistributedCache cache)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _cache = cache;
        }
        public async Task<IActionResult> Index(string? posts)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = posts;
                    return View();
                }
                else
                    return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Login"); }
           
        }
        public async Task<IActionResult> GetIndex(int? category, string? search, string? updated,int PageNumber = 1 )
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    if (search != "" && search != null)
                    {
                        deger = search.Trim();
                        categorys = 0;

                    }
                    else if (category != 0 && category != null)
                    {
                        deger = "";
                        categorys = (int)category;

                    }
                    else if (category == 0)
                    {
                        deger = "";
                        categorys = 0;

                    }
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.screenSize = screenSize;
                    ViewBag.Message = updated;
                    List<ProductDTO> ProductApi = await paginationProduct.GetPagination("api/Product/page-product-list", PageNumber, screenSize, categorys, deger);
                    var Category = await GetCategory();
                    if (Category.Count > 0)
                    {
                        ViewBag.Category = Category;
                    }
                    return View("GetProductIndex", ProductApi);
                } else return RedirectToAction("Index", "Produc");

            } catch { return RedirectToAction("Index", "Produc"); }
           
        }
        public void UserScreenSize(int size)
        {
            screenSize = size;
        }
        public async Task<List<CategoryDTO>> GetCategory()
        {
            try {
                GenericRequests<CategoryDTO> genericRequestsCategory = new GenericRequests<CategoryDTO>();
                var data = await genericRequestsCategory.GetHttpRequest("api/Categories/get-all-category");
                return data;
            }
            catch { return new List<CategoryDTO>(); }
           
        }
        public async Task<IActionResult> GetUpdateIndex(ProductDTO productDTO)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    //string image = productDTO.CategoryName.Remove(productDTO.CategoryName.Length - 4);
                    //productDTO.CategoryName = image;
                    var data = _mapper.Map<NewProductClient>(productDTO);
                    if (!string.IsNullOrEmpty(productDTO.ProductSpacial))
                    { data.EnProductSpacial = await addJsonLanguage.GetValue(productDTO.ProductSpacial, "en-US"); }
                    data.EnProductDetail = await addJsonLanguage.GetValue(productDTO.ProductDetail, "en-US");
                    data.EnProductTitle = await addJsonLanguage.GetValue(productDTO.ProductTitle, "en-US");
                    var category = await GetCategory();
                    foreach (var item in category)
                    {
                        if (item.Id == productDTO.CategoryId)
                        {
                            data.CategoryName = item.CategoryName;
                        }
                    }
                    ViewBag.Category = category;
                    return View("GetUpdateProductIndex", data);
                }
                else
                    return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Login"); }
            
        }
        
        [HttpPost]
        public async Task<IActionResult> PostsProduct(NewProductClient newProduct)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = await PostProductUploadFile(newProduct);
                    var Category = GetCategory();
                    foreach (var item in Category.Result)
                    {
                        if (item.CategoryName == newProduct.CategoryName)
                        {
                            data.CategoryId = item.Id;
                        }
                    }
                    string Posts = await genericRequests.PostRequestGeneric("api/Product/add-product", data);
                    if (Posts == "Başarılı")
                    {
                        AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                        if (!string.IsNullOrEmpty(newProduct.ProductSpacial) && !string.IsNullOrEmpty(newProduct.EnProductSpacial))
                        {
                           await addJsonLanguage.TrSetLanguage(newProduct.ProductSpacial, newProduct.ProductSpacial);
                           await addJsonLanguage.EnSetLanguage(newProduct.ProductSpacial, newProduct.EnProductSpacial);
                        }
                       await addJsonLanguage.TrSetLanguage(newProduct.ProductTitle, newProduct.ProductTitle);
                       await addJsonLanguage.TrSetLanguage(newProduct.ProductDetail, newProduct.ProductDetail);
                       await addJsonLanguage.EnSetLanguage(newProduct.ProductTitle, newProduct.EnProductTitle);
                       await addJsonLanguage.EnSetLanguage(newProduct.ProductDetail, newProduct.EnProductDetail);
                        //PostProductUploadFile
                        return RedirectToAction("Index", "Produc", new { posts = Posts });
                    }
                    else return RedirectToAction("Index", "Produc", new { posts = "Başarısız" });

                }
                else
                    return RedirectToAction("Index", "Login");
            }
            catch (Exception ex) {
                return RedirectToAction("Index", "Produc", new { posts = "Başarısız" });
            }
           
            
        }
        public IActionResult Token(TokenDTO token) 
        {
            tokenDTO = token;
            return RedirectToAction("AdminIndex", "Admin");
        }
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var product = await genericRequests.GetByIdGeneric("api/Product/get-product-by-id", id);
                    string Delete = await deleteRequest.DeleteRequestGeneric("api/Product/delete-product", id);
                    if (Delete == "Başarılı")
                    {
                      await  addJsonLanguage.DeleteLanguage(product.ProductTitle);
                        if (!string.IsNullOrEmpty(product.ProductSpacial))
                        {
                          await  addJsonLanguage.DeleteLanguage(product.ProductSpacial);
                        }
                      await  addJsonLanguage.DeleteLanguage(product.ProductDetail);
                        await FileManager.DeleteFileAsycn(product.ProductImage);
                        await FileManager.DeleteFileAsycn(product.ProductImage2);
                        await FileManager.DeleteFileAsycn(product.ProductImage3);
                        return RedirectToAction("GetIndex", "Produc", new { updated = Delete });
                    }
                    else return RedirectToAction("GetIndex", "Produc", new { updated = "Başarısız" });
                }
                else
                    return RedirectToAction("Index", "Login");

            }
            catch (Exception ex) {
                return RedirectToAction("GetIndex", "Produc", new { updated = "Başarısız" });
            }          
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(NewProductClient newProduct)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var data = await MapProductImage(newProduct);
                    if (data.Id > 0) {
                        if (!string.IsNullOrEmpty(newProduct.ProductSpacial))
                        {
                          await  addJsonLanguage.UpdateLangue(data.ProductSpacial, newProduct.ProductSpacial, newProduct.EnProductSpacial);
                        }
                      await  addJsonLanguage.UpdateLangue(data.ProductTitle, newProduct.ProductTitle, newProduct.EnProductTitle);
                     await   addJsonLanguage.UpdateLangue(data.ProductDetail, newProduct.ProductDetail, newProduct.EnProductDetail);
                        string Update = await genericRequests.UpdateRequestGeneric("api/Product/update-product", data);
                        if (Update == "UpdateBaşarılı") { return RedirectToAction("GetIndex", "Produc", new { updated = Update }); }
                        else return RedirectToAction("GetIndex", "Produc", new { updated = "Başarısız" });
                    }
                    else return RedirectToAction("GetIndex", "Produc", new { updated = "Başarısız" });
                }
                else
                    return RedirectToAction("Index", "Login");
            }
            catch (Exception ex) {
                return RedirectToAction("Index", "Login");
            }
        }
        public async Task<ProductDTO> MapProductImage(NewProductClient newProduct) 
        {
            try {
                ProductDTO ProductApi = await genericRequests.GetByIdGeneric("api/Product/get-product-by-id", (int)newProduct.Id);
                if (ProductApi.Id > 0) 
                {
                    var Category = await GetCategory();
                    var productDto = _mapper.Map<ProductDTO>(newProduct);
                    foreach (var item in Category)
                    {
                        if (item.CategoryName == newProduct.CategoryName)
                        {
                            productDto.CategoryId = item.Id;
                        }
                    }
                    if (newProduct.ProductImage != null && newProduct.ProductImage.FileName != ProductApi.ProductImage)
                    {
                        productDto.ProductImage = await FileManager.UpdateFileAsycn(ProductApi.ProductImage, newProduct.ProductImage);
                    }
                    else { productDto.ProductImage = ProductApi.ProductImage; }
                    if (newProduct.ProductImage2 != null && newProduct.ProductImage2.FileName != ProductApi.ProductImage2)
                    {

                        productDto.ProductImage2 = await FileManager.UpdateFileAsycn(ProductApi.ProductImage2, newProduct.ProductImage2);
                    }
                    else { productDto.ProductImage2 = ProductApi.ProductImage2; }
                    if (newProduct.ProductImage3 != null && newProduct.ProductImage3.FileName != ProductApi.ProductImage3)
                    {
                        productDto.ProductImage3 = await FileManager.UpdateFileAsycn(ProductApi.ProductImage3, newProduct.ProductImage3);
                    }
                    else { productDto.ProductImage3 = ProductApi.ProductImage3; }
                    return productDto;
                }
                else return new ProductDTO();
            }
            catch { return new ProductDTO(); }
            
        }
        public async Task<ProductDTO> PostProductUploadFile(NewProductClient newProduct)
        {
            try {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(newProduct);
                if (newProduct.ProductImage != null)
                {
                    productDTO.ProductImage = await FileManager.PostFileAsycn(newProduct.ProductImage);
                }
                if (newProduct.ProductImage2 != null)
                {
                    productDTO.ProductImage2 = await FileManager.PostFileAsycn(newProduct.ProductImage2);
                }
                if (newProduct.ProductImage3 != null)
                {
                    productDTO.ProductImage3 = await FileManager.PostFileAsycn(newProduct.ProductImage3);
                }
                return productDTO;
            }
            catch (Exception ex)
            {
              return new ProductDTO();
            }

            
        }
    }
}

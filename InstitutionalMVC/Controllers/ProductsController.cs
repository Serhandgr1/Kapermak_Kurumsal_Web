using EntitiesLayer;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InstitutionalMVC.Controllers
{
	public class ProductsController : Controller
	{
        private readonly IStringLocalizer<HomeController> _loc;
        GenericRequests<ProductDTO> genericRequests = new GenericRequests<ProductDTO>();
        public ProductsController(IStringLocalizer<HomeController> loc)
        {
            _loc = loc;
        }
        private static int screenSize;
        private static string deger;
        private static int categorys;
        private static int pageNumber=1;
        public async Task<IActionResult> Index(int? category , string? search, int PageNumber=1 )
        {

            GetRequest<ProductDTO> https = new GetRequest<ProductDTO>();
            List<ProductDTO> product = new List<ProductDTO>();
            List<CategoryDTO> categories = new List<CategoryDTO>();
            ViewBag.PageNumber = PageNumber;
            ViewBag.screenSize = screenSize;
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
                string ProductUrl = Extancion.Client.BaseAddress + "api/Product/page-product-list";
                HttpResponseMessage ProductResponce = Extancion.Client.GetAsync($"{ProductUrl}?PageNumber={PageNumber}&PageSize={screenSize}&category={categorys}&search={deger}").Result;
                List<ProductDTO> ProductApi = await ProductResponce.Content.ReadFromJsonAsync<List<ProductDTO>>();
                product = ProductApi;

            string ProductCategoryUrl = Extancion.Client.BaseAddress + "api/Categories/get-all-category";
                HttpResponseMessage ProductCategoryResponce = Extancion.Client.GetAsync(ProductCategoryUrl).Result;
                List<CategoryDTO> Category = await ProductCategoryResponce.Content.ReadFromJsonAsync<List<CategoryDTO>>();
                categories = Category;

                ViewBag.Category= categories;

            return View("Index", product);
        }
        public IActionResult ProductDetail(int Id)
        {
            var product = genericRequests.GetByIdGeneric("api/Product/get-product-by-id", Id);
            return View("ProductDetail", product.Result);
        }
        public void UserScreenSize(int size) 
        {
            screenSize = size;
        }
       
    }
}

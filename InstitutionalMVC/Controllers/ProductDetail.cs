using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Controllers
{
    public class ProductDetail : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Post(ProductDTO product)
        {
            return View();
        }
    }
}

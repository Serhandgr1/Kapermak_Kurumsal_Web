using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Controllers
{
    public class HeaderController : Controller
    {
        public IActionResult Index()
        {
            return PartialView("HeaderIndex");
        }
    }
}

using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        private static TokenDTO tokenDTO;
        public async Task<IActionResult> Index()
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                return View("IndexAdmin");
            }
               else return RedirectToAction("Index", "Login");
        }
         public async Task<IActionResult> TokenDelete()
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                tokenDTO = new TokenDTO();
                Extancion.Client.DefaultRequestHeaders.Remove("Authorization");
            }
            return RedirectToAction("Index", "Login");

        }

        }
}

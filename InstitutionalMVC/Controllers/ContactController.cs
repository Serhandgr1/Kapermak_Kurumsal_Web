using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Net.Http.Json;

namespace InstitutionalMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IStringLocalizer<HomeController> _loc;
        public ContactController(IStringLocalizer<HomeController> loc)
        {
            _loc = loc;
        }
        public IActionResult Index(string?post)
        {

            ViewBag.Post = post;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostMail(ContactDTO contact)
        {
            string url = Extancion.Client.BaseAddress + "api/ContactApi/PostMail";

            await Extancion.Client.PostAsJsonAsync(url, contact);
            string posts = "Başarılı";
            // send e mail admin
            return RedirectToAction("Index", new { post = posts });
        }

        
    }
}

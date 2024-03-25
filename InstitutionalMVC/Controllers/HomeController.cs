using EntitiesLayer.ModelDTO;
using Institutional;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using InstitutionalMVC.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace InstitutionalMVC.Controllers
{
	public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _loc;
        //GenericRequests<ContactAdminDTO> request = new GenericRequests<ContactAdminDTO>();
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> loc)
		{
			_logger = logger;
            _loc = loc;
		}
    
        public async Task<IActionResult> Index()
        {
            
            GetRequest< CommentDTO > command= new GetRequest< CommentDTO >();
            List<CommentDTO> CommendApi = await command.GetHttpRequest("api/Commend/get-all-commend");
            GetRequest<ProjectDTO> project = new GetRequest<ProjectDTO>();
            List<ProjectDTO> ProjectApi = await project.GetHttpRequest("api/Project/get-all-project");
          
            GetRequest<PreferenceDTO> preferance = new GetRequest<PreferenceDTO>();
            List<PreferenceDTO> PreferanceApi = await preferance.GetHttpRequest("api/Preferance/get-all-preferance");

            return View("Index" , Tuple.Create(ProjectApi, CommendApi, PreferanceApi));

		}
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Language.Name = culture;
            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
              );
            //return RedirectToAction("Index");
            return LocalRedirect(returnUrl);
        }

    }
}
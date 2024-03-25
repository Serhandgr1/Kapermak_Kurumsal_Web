using Azure;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NuGet.Protocol;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace InstitutionalMVC.Controllers
{
	public class AboutController : Controller
    {
        private readonly IStringLocalizer<HomeController> _loc;
        public AboutController(IStringLocalizer<HomeController> loc)
        {
            _loc = loc;
        }
        public async Task<IActionResult> Index()
        {
            //ReferanceApi request
            GetRequest<ReferangeDTO> requestReferance = new GetRequest<ReferangeDTO>();
            List<ReferangeDTO> ReferanceApi = await requestReferance.GetHttpRequest("api/Referance/get-all-referance");
            
            //AbouteApi request
            GetRequest<AboutDTO> request = new GetRequest<AboutDTO>();
            List<AboutDTO> AbouteApi = await request.GetHttpRequest("api/Aboute/get-all-about");

            return View("Index", Tuple.Create(AbouteApi, ReferanceApi));
		}
	}
}

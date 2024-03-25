using EntitiesLayer.ModelDTO;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace InstitutionalMVC.Controllers
{
    public class FooterController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            GetRequest<ContactAdminDTO> request = new GetRequest<ContactAdminDTO>();
            List<ContactAdminDTO> adminContactApi = await request.GetHttpRequest("api/ContactAdmin/get-all-contact-admin");
            return PartialView("FooterIndex" , adminContactApi);
        }
    }
}

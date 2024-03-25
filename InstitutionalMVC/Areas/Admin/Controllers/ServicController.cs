using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ServicController : Controller
    {
        GenericRequests<ServicesDTO> genericRequests = new GenericRequests<ServicesDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public ServicController(IDistributedCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }
        public IActionResult Index(string? posts)
        {
           
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = posts;
                    return View("ServiceIndex");
                }
                 else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllServiceIndex(string? updated)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = updated;
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    List<NewServiceDto> newServiceDtos = new List<NewServiceDto>();
                    var data = await genericRequests.GetHttpRequest("api/Service/get-all-service");
                    foreach (var item in data)
                    {
                        var ser = _mapper.Map<NewServiceDto>(item);
                        ser.EnServiceDetail = await addJsonLanguage.GetValue(item.ServiceDetail, "en-US");
                        ser.EnServiceTitle = await addJsonLanguage.GetValue(item.ServiceTitle, "en-US");
                        ser.ServiceTitle = await addJsonLanguage.GetValue(item.ServiceTitle, "tr-TR");
                        ser.ServiceDetail = await addJsonLanguage.GetValue(item.ServiceDetail, "tr-TR");
                        newServiceDtos.Add(ser);
                    }
                    return View("GetServiceIndex", newServiceDtos);
                }
                else return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Servic"); }
            
        }
        public async Task<IActionResult> GetUpdateServiceIndex(NewServiceDto services)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    string image = services.ServiceImage.Remove(services.ServiceImage.Length - 4);
                    services.ServiceImage = image;
                    return View("GetUpdateService", services);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllServiceIndex", "Servic"); }
                    
        }
        public async Task<IActionResult> PostsService(NewServiceDto services)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(services.ServiceTitle, services.ServiceTitle);
                    await addJsonLanguage.TrSetLanguage(services.ServiceDetail, services.ServiceDetail);
                    await addJsonLanguage.EnSetLanguage(services.ServiceTitle, services.EnServiceTitle);
                    await addJsonLanguage.EnSetLanguage(services.ServiceDetail, services.EnServiceDetail);
                    switch (services.ServiceImage)
                    {
                        case "Montaj": services.ServiceImage = "Montaj.png"; break;
                        case "Mühendis": services.ServiceImage = "Mühendis.png"; break;
                        case "Üretim": services.ServiceImage = "Üretim.png"; break;
                        case "Taşıyıcı": services.ServiceImage = "Taşıyıcı.png"; break;
                    }
                    string posts = await genericRequests.PostRequestGeneric("api/Service/post-service", services);
                    return RedirectToAction("Index", "Servic", new { posts = posts });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Servic", new { posts = "Başarısız" }); }
                    
        }
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var service = genericRequests.GetByIdGeneric("api/Service/get-service-by-id", id);
                    await addJsonLanguage.DeleteLanguage(service.Result.ServiceTitle);
                    await addJsonLanguage.DeleteLanguage(service.Result.ServiceDetail);
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Service/delete-service", id);
                    return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = delete });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = "Başarısız" }); }
                    
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(NewServiceDto services)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var service = genericRequests.GetByIdGeneric("api/Service/get-service-by-id", services.Id);
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.UpdateLangue(service.Result.ServiceTitle, services.ServiceTitle, services.EnServiceTitle);
                    await addJsonLanguage.UpdateLangue(service.Result.ServiceDetail, services.ServiceDetail, services.EnServiceDetail);
                    switch (services.ServiceImage)
                    {
                        case "Montaj": services.ServiceImage = "Montaj.png"; break;
                        case "Mühendis": services.ServiceImage = "Mühendis.png"; break;
                        case "Üretim": services.ServiceImage = "Üretim.png"; break;
                        case "Taşıyıcı": services.ServiceImage = "Taşıyıcı.png"; break;
                    }
                    var update = await genericRequests.UpdateRequestGeneric("api/Service/update-service", services);
                    return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = update });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = "Başarısız" }); }
        }
    }
}
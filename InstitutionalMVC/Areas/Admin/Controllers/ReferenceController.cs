using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ReferenceController : Controller
    {
        UploadFiles uploadFiles = new UploadFiles();
        GenericRequests<ReferangeDTO> genericRequests = new GenericRequests<ReferangeDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        DeleteFiles deleteFiles = new DeleteFiles();
        FileManagerAsycn FileManager = new FileManagerAsycn();
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReferenceController(IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
        
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string? posts)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = posts;
                return View("ReferanceIndex");
            }
            else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllReferanceIndex(string? updated)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = updated;
                    var data = await genericRequests.GetHttpRequest("api/Referance/get-all-referance");
                    return View("GetReferenceIndex", data);
                }
                else return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Reference"); }
            
        }
        public async Task<IActionResult> GetUpdateReferance(ReferangeDTO referange)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = _mapper.Map<NewReferanceClient>(referange);
                    ViewBag.Image = referange.ReferangeImage;
                    return View("GetUpdateReferance", data);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = "Başarısız" }); }
                   
        }
        [HttpPost]
        public async Task<IActionResult> PostsReferance(NewReferanceClient newReferance)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = _mapper.Map<ReferangeDTO>(newReferance);
                    if (newReferance.ReferangeImage != null)
                    {
                        data.ReferangeImage = await FileManager.PostFileAsycn(newReferance.ReferangeImage);
                    }
                    string Posts = await genericRequests.PostRequestGeneric("api/Referance/post-referance", data);
                    return RedirectToAction("Index", "Reference", new { posts = Posts });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Reference", new { posts = "Başarısız" }); }  
                    
        }
        public async Task<IActionResult> DeleteReferance(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ReferangeDTO ReferanceApi = await genericRequests.GetByIdGeneric("api/Referance/get-referance-by-id", id);
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Referance/delete-referance", id);
                    if (delete == "Başarılı")
                    {
                        await FileManager.DeleteFileAsycn(ReferanceApi.ReferangeImage);
                        return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = delete });
                    }
                    else return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = "Başarısız" });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = "Başarısız" }); }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReferance(NewReferanceClient newReferance)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ReferangeDTO ReferanceApi = await genericRequests.GetByIdGeneric("api/Referance/get-referance-by-id", (int)newReferance.Id);
                    var dto = _mapper.Map<ReferangeDTO>(newReferance);
                    if (newReferance.ReferangeImage != null && newReferance.ReferangeImage.FileName != ReferanceApi.ReferangeImage)
                    {
                        dto.ReferangeImage = await FileManager.UpdateFileAsycn(ReferanceApi.ReferangeImage, newReferance.ReferangeImage);
                    }
                    else { dto.ReferangeImage = ReferanceApi.ReferangeImage; }
                    string update = await genericRequests.UpdateRequestGeneric("api/Referance/update-referance", dto);
                    return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = update });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = "Başarısız" }); }
        }
    }
}

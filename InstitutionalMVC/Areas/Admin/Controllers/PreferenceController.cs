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
    public class PreferenceController : Controller
    {
        GenericRequests<PreferenceDTO> genericRequests = new GenericRequests<PreferenceDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        public PreferenceController(IDistributedCache cache, IMapper mapper)
        {
            _mapper = mapper;
            _cache = cache;
        }
        public IActionResult Index(string? post)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = post;
                return View("PreferenceIndex");
            }
            else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllPreferanceIndex(string? updated)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    List<NewPreferanceDto> newPreferanceDtos = new List<NewPreferanceDto>();
                    ViewBag.Message = updated;
                    var data = await genericRequests.GetHttpRequest("api/Preferance/get-all-preferance");
                    foreach (var item in data)
                    {
                        var pref = _mapper.Map<NewPreferanceDto>(item);
                        pref.PreferenceTitle = await addJsonLanguage.GetValue(item.PreferenceTitle, "tr-TR");
                        pref.PreferenceDetail = await addJsonLanguage.GetValue(item.PreferenceDetail, "tr-TR");
                        pref.EnPreferenceDetail = await addJsonLanguage.GetValue(item.PreferenceDetail, "en-US");
                        pref.EnPreferenceTitle = await addJsonLanguage.GetValue(item.PreferenceTitle, "en-US");
                        newPreferanceDtos.Add(pref);
                    }
                    return View("GetPreferenceIndex", newPreferanceDtos);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Preference"); }
            
        }
        public async Task<IActionResult> GetUpdatePreferanceIndex(NewPreferanceDto preferance)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    string image = preferance.PreferenceImage.Remove(preferance.PreferenceImage.Length - 4);
                    preferance.PreferenceImage = image;
                    return View("GetUpdatePreferance", preferance);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllPreferanceIndex", "Preference"); }
                   
        }
        public async Task<IActionResult> PostsPreference(NewPreferanceDto preference)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(preference.PreferenceTitle, preference.PreferenceTitle);
                    await addJsonLanguage.TrSetLanguage(preference.PreferenceDetail, preference.PreferenceDetail);
                    await addJsonLanguage.EnSetLanguage(preference.PreferenceTitle, preference.EnPreferenceTitle);
                    await addJsonLanguage.EnSetLanguage(preference.PreferenceDetail, preference.EnPreferenceDetail);
                    switch (preference.PreferenceImage)
                    {
                        case "Hız": preference.PreferenceImage = "Hız.png"; break;
                        case "İletişim": preference.PreferenceImage = "İletişim.png"; break;
                        case "Kadro": preference.PreferenceImage = "Kadro.png"; break;
                        case "İşletme": preference.PreferenceImage = "İşletme.png"; break;
                    }
                    string posts = await genericRequests.PostRequestGeneric("api/Preferance/post-preferance", preference);
                    return RedirectToAction("Index", "Preference", new { post = posts });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Preference", new { post = "başarısız" }); }
            
        }
        public async Task<IActionResult> DeletePreference(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var prefer = genericRequests.GetByIdGeneric("api/Preferance/get-preferance-by-id", id);
                    await addJsonLanguage.DeleteLanguage(prefer.Result.PreferenceTitle);
                    await addJsonLanguage.DeleteLanguage(prefer.Result.PreferenceDetail);
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Preferance/delete-preferance", id);
                    return RedirectToAction("GetAllPreferanceIndex", "Preference", new { updated = delete });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllPreferanceIndex", "Preference", new { updated = "Başarısız" }); }
                    
        }
        public async Task<IActionResult> UpdatePreference(NewPreferanceDto preference)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = await genericRequests.GetByIdGeneric("api/Preferance/get-preferance-by-id", preference.Id);
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.UpdateLangue(data.PreferenceTitle, preference.PreferenceTitle, preference.EnPreferenceTitle);
                    await addJsonLanguage.UpdateLangue(data.PreferenceDetail, preference.PreferenceDetail, preference.EnPreferenceDetail);
                    switch (preference.PreferenceImage)
                    {
                        case "Hız": preference.PreferenceImage = "Hız.png"; break;
                        case "İletişim": preference.PreferenceImage = "İletişim.png"; break;
                        case "Kadro": preference.PreferenceImage = "Kadro.png"; break;
                        case "İşletme": preference.PreferenceImage = "İşletme.png"; break;
                    }
                    string update = await genericRequests.UpdateRequestGeneric("api/Preferance/update-preferance", preference);
                    return RedirectToAction("GetAllPreferanceIndex", "Preference", new { updated = update });
                }
                else return RedirectToAction("Index", "Login");
            }catch { return RedirectToAction("GetAllPreferanceIndex", "Preference", new { updated = "Başarısız" }); }
                   
        }
    }
}

using AutoMapper;
using EntitiesLayer.ModelDTO;
using System.IO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Hosting;
using Humanizer;
using Elfie.Serialization;
using NuGet.ContentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using Institutional.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class AboutsController : Controller
    {
        private readonly IDistributedCache _cache;
        UploadFiles uploadFiles = new UploadFiles();
        DeleteFiles deleteFiles = new DeleteFiles();
        FileManagerAsycn FileManager = new FileManagerAsycn();
        GenericRequests<AboutDTO> genericRequests = new GenericRequests<AboutDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AboutsController(IMapper mapper , IWebHostEnvironment webHostEnvironment , IDistributedCache cache)
        {
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _cache = cache;
        }
       
        public IActionResult Index(string? posts)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = posts;
                return View("AbouteIndex");
            }
            else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllIndex(string? updated)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    List<NewAboutClient> newAboutClients = new List<NewAboutClient>();
                    ViewBag.Message = updated;
                    var Aboute = genericRequests.GetHttpRequest("api/Aboute/get-all-about");
                    foreach (var item in Aboute.Result)
                    {
                        var data = _mapper.Map<NewAboutClient>(item);

                        data.Title = await addJsonLanguage.GetValue(item.Title, "tr-TR");
                        data.EnTitle = await addJsonLanguage.GetValue(item.Title, "en-US");
                        data.EnContents = await addJsonLanguage.GetValue(item.Contents, "en-US");
                        data.Contents = await addJsonLanguage.GetValue(item.Contents, "tr-TR");
                        newAboutClients.Add(data);
                    }

                    return View("GetAboutIndex", newAboutClients);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("Index", "Abouts"); }
            

        }
        public async Task<IActionResult> GetUpdateAboutIndex(NewAboutClient about)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    return View("GetUpdateAbout", about);
                }
                else { return RedirectToAction("Index", "Login"); }
                    
            } catch { return RedirectToAction("GetAllIndex", "Abouts"); }
             
        }
       
        [HttpPost]
        public async Task<IActionResult> PostsAboute(NewAboutClient newAbout)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(newAbout.Title, newAbout.Title);
                    await addJsonLanguage.EnSetLanguage(newAbout.Title, newAbout.EnTitle);
                    await addJsonLanguage.TrSetLanguage(newAbout.Contents, newAbout.Contents);
                    await addJsonLanguage.EnSetLanguage(newAbout.Contents, newAbout.EnContents);
                    //Set(newAbout.Contents,newAbout.Contents);
                    var aboutDTO = _mapper.Map<AboutDTO>(newAbout);
                    if (newAbout.Image != null)
                    {

                        aboutDTO.Image = await FileManager.PostFileAsycn(newAbout.Image);
                    }
                    if (newAbout.Image2 != null)
                    {
                        aboutDTO.Image2 = await FileManager.PostFileAsycn(newAbout.Image2);
                    }
                    if (newAbout.Image3 != null)
                    {
                        aboutDTO.Image3 = await FileManager.PostFileAsycn(newAbout.Image3);
                    }
                    string posts = await genericRequests.PostRequestGeneric("api/Aboute/post-aboute", aboutDTO);
                    return RedirectToAction("Index", "Abouts", new { posts = posts });
                }else return RedirectToAction("Index", "Login");

            }
            catch { return RedirectToAction("Index", "Abouts"); }
            
        }
        public async Task<IActionResult> DeleteAboute(int id)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var aboute = await genericRequests.GetByIdGeneric("api/Aboute/get-by-id-about", id);
                    await addJsonLanguage.DeleteLanguage(aboute.Contents);
                    if (!string.IsNullOrEmpty(aboute.Image))
                    {
                        await FileManager.DeleteFileAsycn(aboute.Image);

                    }
                    if (!string.IsNullOrEmpty(aboute.Image2))
                    {
                        await FileManager.DeleteFileAsycn(aboute.Image2);
                    }
                    if (!string.IsNullOrEmpty(aboute.Image3))
                    {
                        await FileManager.DeleteFileAsycn(aboute.Image3);
                    }
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Aboute/delete-aboute", id);
                    return RedirectToAction("GetAllIndex", "Abouts", new { updated = delete });
                }else return RedirectToAction("Index", "Login");


            } catch {return RedirectToAction("GetAllIndex", "Abouts"); }
            
        }
        public async Task<IActionResult> UpdateAboute(NewAboutClient newAbout)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var data = await genericRequests.GetByIdGeneric("api/Aboute/get-by-id-about", newAbout.Id);
                    await addJsonLanguage.UpdateLangue(data.Title, newAbout.Title, newAbout.EnTitle);
                    await addJsonLanguage.UpdateLangue(data.Contents, newAbout.Contents, newAbout.EnContents);
                    var aboutDTO = _mapper.Map<AboutDTO>(newAbout);
                    if (newAbout.Image != null && newAbout.Image.FileName != data.Image)
                    {
                        aboutDTO.Image = await FileManager.UpdateFileAsycn(data.Image, newAbout.Image);
                    }
                    else { aboutDTO.Image = data.Image; }
                    if (newAbout.Image2 != null && newAbout.Image2.FileName != data.Image2)
                    {
                        aboutDTO.Image2 = await FileManager.UpdateFileAsycn(data.Image2, newAbout.Image2);
                    }
                    else
                    { aboutDTO.Image2 = data.Image2; }
                    if (newAbout.Image3 != null && newAbout.Image3.FileName != data.Image3)
                    {
                        aboutDTO.Image3 = await FileManager.UpdateFileAsycn(data.Image3, newAbout.Image3);
                    }
                    else
                    { aboutDTO.Image3 = data.Image3; }
                    string update = await genericRequests.UpdateRequestGeneric("api/Aboute/update-aboute", aboutDTO);
                    return RedirectToAction("GetAllIndex", "Abouts", new { updated = update });
                }else return RedirectToAction("Index", "Login");

            } catch { return RedirectToAction("GetAllIndex", "Abouts", new { updated = "Başarısız" }); }
        }  
    }
}

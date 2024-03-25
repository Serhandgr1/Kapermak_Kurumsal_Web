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
    public class ProjectsController : Controller
    {
        private static int screenSize;
        private static int pageNumber;
        FileManagerAsycn FileManager = new FileManagerAsycn();
        GenericRequests<ProjectDTO> genericRequests = new GenericRequests<ProjectDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDistributedCache _cache;
        public ProjectsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IDistributedCache cache)
        {
            _cache = cache;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string? posts)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = posts;
                return View("ProjectIndex");
            }
            else return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> GetAllProjectIndex(string? updated, int PageNumber = 1)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = updated;
                    pageNumber = PageNumber;
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.screenSize = screenSize;

                    string ProjectUrl = Extancion.Client.BaseAddress + "api/Project/page-project-list";
                    HttpResponseMessage ProductResponce = Extancion.Client.GetAsync($"{ProjectUrl}?PageNumber={pageNumber}&PageSize={screenSize}").Result;
                    List<ProjectDTO> ProjectApi = await ProductResponce.Content.ReadFromJsonAsync<List<ProjectDTO>>();

                    return View("GetProjectIndex", ProjectApi);
                }
                else return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Projects"); }
            
        }

     
        public async Task<IActionResult> GetUpdateProjeIndex(ProjectDTO project)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var data = _mapper.Map<NewProjeClient>(project);
                    data.EnProjectTitle = await addJsonLanguage.GetValue(project.ProjectTitle, "en-US");
                    data.EnProjectDetail = await addJsonLanguage.GetValue(project.ProjectDetail, "en-US");
                    ViewBag.Image = project.ProjectImage;
                    return View("GetUpdateProje", data);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllProjectIndex", "Projects"); }
                    
        }
        [HttpPost]
        public async Task<IActionResult> PostsProject(NewProjeClient newProje)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    await addJsonLanguage.TrSetLanguage(newProje.ProjectTitle, newProje.ProjectTitle);
                    await addJsonLanguage.TrSetLanguage(newProje.ProjectDetail, newProje.ProjectDetail);
                    await addJsonLanguage.EnSetLanguage(newProje.ProjectTitle, newProje.EnProjectTitle);
                    await addJsonLanguage.EnSetLanguage(newProje.ProjectDetail, newProje.EnProjectDetail);
                    var project = _mapper.Map<ProjectDTO>(newProje);
                    if (newProje.ProjectImage != null)
                    {
                        string name = await FileManager.PostFileAsycn(newProje.ProjectImage);
                        project.ProjectImage = name;
                    }
                    string Posts = await genericRequests.PostRequestGeneric("api/Project/post-project", project);
                    return RedirectToAction("Index", "Projects", new { posts = Posts });
                }
                else return RedirectToAction("Index", "Login");
            } catch { return RedirectToAction("Index", "Projects", new { posts = "Başarısız" }); }
            
        }
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {

                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    var project = await genericRequests.GetByIdGeneric("api/Project/get-project-by-id", id);
                    string delete = await deleteRequest.DeleteRequestGeneric("api/Project/delete-project", id);
                    if (delete == "Başarılı")
                    {
                        await FileManager.DeleteFileAsycn(project.ProjectImage);
                        await addJsonLanguage.DeleteLanguage(project.ProjectTitle);
                        await addJsonLanguage.DeleteLanguage(project.ProjectDetail);
                        return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = delete });
                    }
                    else return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = "Başarısız" });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllProjectIndex", "Projects"); }

        }
        public void UserScreenSize(int size)
        {
            screenSize = size;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(NewProjeClient newProje)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    AddJsonLanguage addJsonLanguage = new AddJsonLanguage(_cache);
                    ProjectDTO ProjectApi = await genericRequests.GetByIdGeneric("api/Project/get-project-by-id", (int)newProje.Id);
                    await addJsonLanguage.UpdateLangue(ProjectApi.ProjectTitle, newProje.ProjectTitle, newProje.EnProjectTitle);
                    await addJsonLanguage.UpdateLangue(ProjectApi.ProjectDetail, newProje.ProjectDetail, newProje.EnProjectDetail);
                    var project = _mapper.Map<ProjectDTO>(newProje);
                    if (newProje.ProjectImage != null && newProje.ProjectImage.FileName != ProjectApi.ProjectImage)
                    {
                        project.ProjectImage = await FileManager.UpdateFileAsycn(ProjectApi.ProjectImage, newProje.ProjectImage);
                    }
                    else { project.ProjectImage = ProjectApi.ProjectImage; }
                    string update = await genericRequests.UpdateRequestGeneric("api/Project/update-project", project);
                    return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = update });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = "Başarısız" }); }
                    

        }
    }
}

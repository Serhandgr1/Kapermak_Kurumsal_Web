using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ContactAdminsController : Controller
    {
        GenericRequests<ContactAdminDTO> genericRequests = new GenericRequests<ContactAdminDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                ViewBag.Message = posts;
                return View("ContactAdminsIndex");
            }
            else return RedirectToAction("Index", "Login");

        }
        public  async Task <IActionResult> GetUpdateContactAdmin(ContactAdminDTO contactAdmin) 
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    return View("GetUpdateContactAdmin", contactAdmin);
                }
                else return RedirectToAction("Index", "Login");
            }
            catch  { return RedirectToAction("GetAllAdminContact", "ContactAdmins"); }
                    
        }
        public  async Task<IActionResult> GetAllAdminContact(string? updated)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ViewBag.Message = updated;
                    var data = await genericRequests.GetHttpRequest("api/ContactAdmin/get-all-contact-admin");
                    return View("GetAllAdminContactIndex", data);
                }
                else return RedirectToAction("Index", "Login");
            }
                    catch { return RedirectToAction("Index", "ContactAdmins"); }
        }
        [HttpPost]
        public async Task<IActionResult> PostContactAdmin(ContactAdminDTO contactAdmin)
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = genericRequests.PostRequestGeneric("api/ContactAdmin/create-admin-contact", contactAdmin);
                    return RedirectToAction("Index", "ContactAdmins", new { posts = data });
                }
                else return RedirectToAction("Index", "Login");

            }
            catch { return RedirectToAction("Index", "ContactAdmins"); }
            
        }
        public async Task<IActionResult> UpdateContactAdmin(ContactAdminDTO contactAdmin) 
        {
            try {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = genericRequests.UpdateRequestGeneric("api/ContactAdmin/update-contact-admin", contactAdmin);
                    return RedirectToAction("GetAllAdminContact", "ContactAdmins", new { updated = data });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllAdminContact", "ContactAdmins", new { updated = "Başarısız" }); }
            
        }
        public  async Task<IActionResult> DeleteAdminContact(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = deleteRequest.DeleteRequestGeneric("api/ContactAdmin/delete-contact-admin", id);
                    return RedirectToAction("GetAllAdminContact", "ContactAdmins", new { updated = data });
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllAdminContact", "ContactAdmins", new { updated = "Başarısız" }); }
                 
        }
    }
}

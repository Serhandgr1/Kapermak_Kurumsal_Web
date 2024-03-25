using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ContactsController : Controller
    {
        GenericRequests<ContactDTO> genericRequests = new GenericRequests<ContactDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index()
        {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    return View("ContactIndex");
                }
              else  return RedirectToAction("Index", "Login");        
        }
        public async Task<IActionResult> GetAllContactIndex()
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var data = await genericRequests.GetHttpRequest("api/ContactApi/get-all-contact");
                    return View("GetContactIndex", data);
                }
                else return RedirectToAction("Index", "Login");
                
            }
            catch { return RedirectToAction("Index", "Contacts"); }

        }
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                if (Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    await deleteRequest.DeleteRequestGeneric("api/ContactApi/delete-contact", id);
                    return RedirectToAction("GetAllContactIndex", "Contacts");
                }
                else return RedirectToAction("Index", "Login");
            }
            catch { return RedirectToAction("GetAllContactIndex", "Contacts"); }
                    
        }
    }
} 
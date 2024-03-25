using InstitutionalMVC.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstitutionalMVC.Areas.Client.Controllers
{
    public class FileController : Controller
    {
        UploadFiles uploadFiles = new UploadFiles();
        DeleteFiles deleteFiles = new DeleteFiles();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> FileUpload(IFormFile images)
        {
            try {
                if (images != null) 
                {
                    string name = uploadFiles.UploadFile(images);
                    return name;
                }
                else return "null";

            }
            catch(Exception ex) {
                    return "null";
            }
           
        }
        public async Task<IActionResult> FileDelete(string images)
        {
            try
            {
                if (images != null)
                {
                    deleteFiles.DeleteFile(_webHostEnvironment, images);
                    return Ok();
                }else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<string> FileUpdate(IFormFile file , string? images)
        {
            try
            {
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(images)) { deleteFiles.DeleteFile(_webHostEnvironment, images); }
                    string name = uploadFiles.UploadFile(file);
                    return name;
                }else return "null";
                   
            }
            catch (Exception ex)
            {
                return "null";
            }

        }
        public async Task<IActionResult> LanguageUpload(string key, string value, string filePath) 
        {
            try {
                string json = System.IO.File.ReadAllText(filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                var sectionPath = key.Split(":")[0];
                if (!string.IsNullOrEmpty(sectionPath))
                {
                    //string relativeFilePath = System.IO.File.ReadAllText(_filePath);
                    string fullFilePath = filePath;
                    if (System.IO.File.Exists(fullFilePath))
                    {
                        string deger = jsonObj[sectionPath];
                        if (string.IsNullOrEmpty(deger)) jsonObj[sectionPath] = value;
                    }                    //var keyPath = key.Split(":")[1];
                }
                else
                    jsonObj[sectionPath] = value;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, output);
                return Ok();
            }
            catch(Exception ex) { 
             return BadRequest("null");
            }
           
        }
        public async Task<IActionResult> LanguageDelete(string key, string filePath)
        { try {
                
                string path= Path.Combine(Directory.GetCurrentDirectory(), $"Resources\\{filePath}.json");
                string json = System.IO.File.ReadAllText(path);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                var sectionPath = key.Split(":")[0];
                if (!string.IsNullOrEmpty(sectionPath))
                {
                    //string relativeFilePath = System.IO.File.ReadAllText(_filePath);
                    string fullFilePath = path;
                    if (System.IO.File.Exists(fullFilePath))
                    {
                        string deger = jsonObj[sectionPath];
                        if (!string.IsNullOrEmpty(deger))
                        {
                            JProperty idProp = jsonObj.Property(key);
                            idProp.Remove();
                            string updatedJson = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                            System.IO.File.WriteAllText(path, updatedJson);
                        }
                    }
                }
                return Ok();
            }
            catch(Exception ex) { return BadRequest(); }
           
        }

    }
}

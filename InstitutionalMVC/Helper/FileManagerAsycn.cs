using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Helper
{
    public class FileManagerAsycn
    {
        public async Task<string> PostFileAsycn(IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string urlReuest = "https://localhost:7083/" + "File/FileUpload";
                    using var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(formFile.OpenReadStream()), "images", formFile.FileName);
                    HttpResponseMessage responce = await Extancion.Client.PostAsync(urlReuest, content);
                    string data = await responce.Content.ReadAsStringAsync();
                    return data;
                }
                else return "null";

            }
            catch (Exception ex) { return "null"; }

        }
        public async Task<string> UpdateFileAsycn(string? images, IFormFile formFile)
        {
            try
            {

                if (formFile != null)
                {
                    string urlReuest = "https://localhost:7083/" + "File/FileUpdate";
                    using var content = new MultipartFormDataContent();
                    content.Add(new StringContent(images), "images");
                    content.Add(new StreamContent(formFile.OpenReadStream()), "file", formFile.FileName);
                    HttpResponseMessage responce = await Extancion.Client.PostAsync(urlReuest, content);
                    string data = await responce.Content.ReadAsStringAsync();
                    return data;
                }
                else return "null";

            }
            catch (Exception e)
            {
                return "null";
            }
        }
        public async Task DeleteFileAsycn(string images)
        {
            try
            {
                if (!string.IsNullOrEmpty(images))
                {
                    string urlReuest = "https://localhost:7083/" + "File/FileDelete";
                    using var content = new MultipartFormDataContent();
                    content.Add(new StringContent(images), "images");
                    HttpResponseMessage responce = await Extancion.Client.PostAsync(urlReuest, content);
                }
 
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}

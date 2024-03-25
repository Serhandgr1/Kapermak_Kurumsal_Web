using Microsoft.AspNetCore.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.Helper
{
    public class DeleteFiles
    {
        public void DeleteFile(IWebHostEnvironment webHostEnvironment , string image)
        {
            var url = webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\");
            string path = Path.Combine(url, image);
            System.IO.File.Delete(path);
        }
    }
}

using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private static TokenDTO tokenDTO;
        public IActionResult Index()
        {
            if (!Extancion.Client.DefaultRequestHeaders.Contains("Authorization")) 
            {
                return View(); 
            }
            else 
            {
                return RedirectToAction("Index", "Admin"); 
            }
           
        }
        public IActionResult BadLogin()
        {
                string Baddata = "Bilgileriniz hatalıdır kontrol ederek tekrar deneyin";
                ViewBag.Baddata = Baddata;  
            return View("Index");
        }
        public async Task<IActionResult> LoginChack(UserForAuthenticationDTO userForAuthentication)
        {
            try {
                if (!Extancion.Client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    string url = Extancion.Client.BaseAddress + "api/Authentication/login";
                    var data = await Extancion.Client.PostAsJsonAsync(url, userForAuthentication);
                    tokenDTO = await data.Content.ReadFromJsonAsync<TokenDTO>();
                    if (tokenDTO.AccessToken != null)
                    {
                        Extancion.Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenDTO.AccessToken);
                        ClientTokenDto.AccessToken = tokenDTO.AccessToken;
                        ClientTokenDto.RefreshToken = tokenDTO.RefreshToken;
                    }

                    if (data.ReasonPhrase == "OK")
                    {

                        ViewBag.Token = tokenDTO.AccessToken;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        //string Baddata = "Bilgileriniz hatalıdır kontrol ederek tekrar deneyin";
                        return RedirectToAction("BadLogin", "Login");
                    }
                }
                return RedirectToAction("Index", "Admin");
            }
            catch { return RedirectToAction("Index", "Login"); }
        }
        }
}

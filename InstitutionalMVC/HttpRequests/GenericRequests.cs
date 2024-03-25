using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstitutionalMVC.HttpRequests
{
    public class GenericRequests<T>
    {
        RefreshTokenDto refreshTokenDto = new RefreshTokenDto();
        public async Task<List<T>> GetHttpRequest(string url, string? search = "", int? category = 0)
        {
            string ProductCategoryUrl = Extancion.Client.BaseAddress + url;
            HttpResponseMessage ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}").Result;
            switch (((int)ProductCategoryResponce.StatusCode)) 
            {
               case 401:
                        bool again = await refreshTokenDto.Refresh();
                        if (again)
                        {
                            var data = await GetHttpRequest(url, search = "", category = 0);
                            return data.ToList();
                        }
                        else
                        {
                            return new List<T>();
                        };
               case 200:
                        if (category != null && category != 0)
                        {
                            ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}{category}").Result;
                        }
                        else if (search != null && search != "")
                        {
                            ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}{search}").Result;
                        }

                        List<T> ProductCategoryApi = await ProductCategoryResponce.Content.ReadFromJsonAsync<List<T>>();
                        return ProductCategoryApi;
                default: return new List<T>();
            }
        }
        public async Task<string> PostRequestGeneric(string Url, T entity)
        {
            string url = Extancion.Client.BaseAddress + Url;
            var data = await Extancion.Client.PostAsJsonAsync(url, entity);

            switch (((int)data.StatusCode)) 
            {
                case 200: return "Başarılı";
                case 401:
                    bool again = await refreshTokenDto.Refresh();
                    if (again)
                    {
                        var agn = await PostRequestGeneric(Url, entity);
                        return agn.ToString();
                    }
                    else return "Başarısız";
                default: return "Başarısız"; ;
            }
        }
    
        public async Task<string> UpdateRequestGeneric(string url, T entity)
        {
            string urlUpdate = Extancion.Client.BaseAddress + url;
            var data = await Extancion.Client.PutAsJsonAsync(urlUpdate, entity);

            switch (((int)data.StatusCode)) 
            {
                case 200: return "UpdateBaşarılı";
                case 401:
                    bool again = await refreshTokenDto.Refresh();
                    if (again)
                    {
                        var agn = await UpdateRequestGeneric(url, entity);
                        return agn.ToString();
                    }
                    else
                        return "Başarısız"; ;
                default: return "Başarısız"; ;
            
                }
        }
        public  async Task<T> GetByIdGeneric(string url , int id)
        {
            string urlReuest = Extancion.Client.BaseAddress + url;
            HttpResponseMessage responce = Extancion.Client.GetAsync($"{urlReuest}?id={id}").Result;
            switch (((int)responce.StatusCode)) 
            {
                case 200:
                    T dto = await responce.Content.ReadFromJsonAsync<T>();
                    return dto; ;
                case 401:
                    bool again = await refreshTokenDto.Refresh();
                    if (again)
                    {
                        var agn = await GetByIdGeneric(url, id);
                        return agn;
                    }
                    else
                    {
                        T obj = new List<T>().First();
                        return obj;
                    };
                default:
                    T objd = new List<T>().First();
                    return objd; ;
            }
        }
    }
}

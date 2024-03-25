using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class GetRequest<T>
    {
        RefreshTokenDto refreshTokenDto = new RefreshTokenDto();
        public async Task<List<T>> GetHttpRequest(string url , string? search="" , int? category=0) 
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var data = await Extancion.Client.SendAsync(request);

            string ProductCategoryUrl = Extancion.Client.BaseAddress + url;
                       HttpResponseMessage ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}").Result;
            if (((int)ProductCategoryResponce.StatusCode) == 401)
            {
                bool again = await refreshTokenDto.Refresh();
                if (again)
                {
                    await GetHttpRequest(url, search = "", category = 0);
                }
                else
                {
                    return new List<T>();
                }
            }
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
        }
    }
}

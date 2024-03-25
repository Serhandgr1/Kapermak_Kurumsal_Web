using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class GetPaginationProduct
    {
        public async Task<List<ProductDTO>> GetPagination(string url , int PageNumber , int screenSize , int? categorys  , string? deger) 
        {
            string ProductUrl = Extancion.Client.BaseAddress + url;
            HttpResponseMessage ProductResponce = Extancion.Client.GetAsync($"{ProductUrl}?PageNumber={PageNumber}&PageSize={screenSize}&category={categorys}&search={deger}").Result;
            List<ProductDTO> ProductApi = await ProductResponce.Content.ReadFromJsonAsync<List<ProductDTO>>();
            return ProductApi;
        }
    }
}

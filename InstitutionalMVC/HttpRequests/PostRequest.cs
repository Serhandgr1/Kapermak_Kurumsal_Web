using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class PostRequest<T>
    {
        RefreshTokenDto refreshTokenDto = new RefreshTokenDto();
        public async Task PostRequestGeneric(string Url, T entity) 
        {
            string url = Extancion.Client.BaseAddress + Url;
            var data = await Extancion.Client.PostAsJsonAsync(url, entity);
            if (((int)data.StatusCode) == 401)
            {
                bool again = await refreshTokenDto.Refresh();
                if (again)
                {
                    await PostRequestGeneric(Url, entity);
                }
               
            }
          
        }
    }
}

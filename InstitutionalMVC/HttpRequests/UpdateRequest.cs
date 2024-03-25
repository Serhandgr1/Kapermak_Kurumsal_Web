using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class UpdateRequest<T>
    {
        RefreshTokenDto refreshTokenDto = new RefreshTokenDto();
        public async Task UpdateRequestGeneric(string url , T entity) 
        {
            string urlUpdate = Extancion.Client.BaseAddress + url;
            var data = await Extancion.Client.PutAsJsonAsync(urlUpdate, entity);
            if (((int)data.StatusCode) == 401)
            {
                bool again = await refreshTokenDto.Refresh();
                if (again)
                {
                    await UpdateRequestGeneric(url, entity);
                }
             
            }

        }
    }
}

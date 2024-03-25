using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class DeleteRequest
    {
        RefreshTokenDto refreshTokenDto = new RefreshTokenDto();
        public async Task<string> DeleteRequestGeneric(string url ,int id ) 
        {
            string urlDelete = Extancion.Client.BaseAddress + url;
            var data = Extancion.Client.DeleteAsync($"{urlDelete}?id={id}").Result;
            if (((int)data.StatusCode) == 401)
            {
                bool again = await refreshTokenDto.Refresh();
                if (again)
                {
                    await DeleteRequestGeneric(url, id);
                }
                else
                {
                    return "Başarısız";
                }
            }
            return "Başarılı";
        }
    }
}

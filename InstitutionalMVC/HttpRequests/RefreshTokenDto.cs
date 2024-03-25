using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class RefreshTokenDto
    {
        public async Task<bool> Refresh()
        {
            var urlRef = Extancion.Client.BaseAddress + "api/Authentication/refresh";
            TokenDTO tokenDto = new TokenDTO();
            tokenDto.RefreshToken = ClientTokenDto.RefreshToken;
            tokenDto.AccessToken = ClientTokenDto.AccessToken;
            var token = await Extancion.Client.PostAsJsonAsync(urlRef, tokenDto);
            if (token.IsSuccessStatusCode)
            {
                TokenDTO newToken = await token.Content.ReadFromJsonAsync<TokenDTO>();
                Extancion.Client.DefaultRequestHeaders.Remove("Authorization");
                Extancion.Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + newToken.AccessToken);
                ClientTokenDto.AccessToken = newToken.AccessToken;
                ClientTokenDto.RefreshToken = newToken.RefreshToken;
                return true;
            }
            return false;
        }
    }
}

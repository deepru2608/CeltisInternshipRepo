using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IntershipTaskFront.Models;
using Newtonsoft.Json;

namespace IntershipTaskFront.Services
{
    public class CenterService : ICenterService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CenterService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var userData = _tokenService.GetCredential();
            var token = await _tokenService.GetToken(userData);
            var requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Get;
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.SendAsync(requestMessage);
            IEnumerable<Item> result = new List<Item>();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<IEnumerable<Item>>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return result;
        }
    }
}
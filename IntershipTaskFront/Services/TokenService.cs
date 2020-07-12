using System.Diagnostics.SymbolStore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IntershipTaskFront.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IntershipTaskFront.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        
        public UserData GetCredential()
        {
            var username = _configuration["UserData:Username"];
            var password = _configuration["UserData:Password"];
            return new UserData
            {
                Username = username,
                Password = password
            };
        }

        public async Task<string> GetToken(UserData userData)
        {
            string serializedCode = JsonConvert.SerializeObject(userData);
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(serializedCode)
            };
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(requestMessage);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
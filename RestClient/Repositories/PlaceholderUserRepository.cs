using Application.Repositories;
using Domain.DTO;
using Domain.Entities;
using Domain.ValueObjects;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Json;

namespace RestClient.Repositories
{
    internal class PlaceholderUserRepository(RestClient _restClient, IHttpClientFactory httpClientFactory, IConfiguration configuration) : IPlaceholderUserRepository
    {
        private const string BaseUrl = "https://localhost:7077/api/";

        private async Task<string> GetToken()
        {
            using var httpClient = httpClientFactory.CreateClient();
            var requestToken = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(configuration["Url:SSO"] ?? string.Empty)
            };
            httpClient.DefaultRequestHeaders.Add("clientid", configuration["PublicToken:ClientId"]);
            httpClient.DefaultRequestHeaders.Add("clientsecret", configuration["PublicToken:ClientSecret"]);
            var response = await httpClient.SendAsync(requestToken);
            if (response.Content == null)
            {
                throw new Exception("Login gagal");
            }
            var resultContent = await response.Content.ReadAsStringAsync();
            var token = "";
            if (resultContent != null && response.StatusCode == HttpStatusCode.OK)
            {
                dynamic? loginModelToken = JsonConvert.DeserializeObject(resultContent);
                token = loginModelToken?.token ?? string.Empty;
            }
            else
            {
                throw new Exception("Token gagal");
            }

            return token;
        }

        private async Task<HttpClient> GetHttpClient()
        {
            var token = await GetToken();
            var client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl),

            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<List<Course>?> GetAll()
        {
            using var client = await GetHttpClient();
            return await client.GetFromJsonAsync<List<Course>>("Courses");
        }

        public async Task<PlaceholderUser?> GetById(int id)
        {
            return await _restClient.Get<PlaceholderUser?>(BaseUrl + "users/" + id);
        }
    }
}

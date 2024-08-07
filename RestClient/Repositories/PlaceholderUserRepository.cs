using Application.Repositories;
using Domain.ValueObjects;

namespace RestClient.Repositories
{
    internal class PlaceholderUserRepository(RestClient _restClient) : IPlaceholderUserRepository
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/";

        public async Task<List<PlaceholderUser>?> GetAll()
        {
            return await _restClient.Get<List<PlaceholderUser>>(BaseUrl + "users");
        }

        public async Task<PlaceholderUser?> GetById(int id)
        {
            return await _restClient.Get<PlaceholderUser?>(BaseUrl + "users/" + id);
        }
    }
}

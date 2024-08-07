using Domain.ValueObjects;

namespace Application.Repositories
{
    public interface IPlaceholderUserRepository
    {
        public Task<List<PlaceholderUser>?> GetAll();
        public Task<PlaceholderUser?> GetById(int idd);
    }
}

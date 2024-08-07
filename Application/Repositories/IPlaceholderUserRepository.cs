using Domain.DTO;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Repositories
{
    public interface IPlaceholderUserRepository
    {
        public Task<List<Course>?> GetAll();
        public Task<PlaceholderUser?> GetById(int idd);
    }
}

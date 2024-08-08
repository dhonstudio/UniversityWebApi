using Domain.DTO;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Repositories
{
    public interface IPlaceholderUserRepository
    {
        public Task<List<PlaceholderUser>?> GetAll();
        public Task<List<Student>?> GetFromStudent();
        public Task<PlaceholderUser?> GetById(int idd);
    }
}

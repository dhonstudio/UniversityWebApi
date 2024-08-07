using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class StudentFeature(IRepository<Student> _studentRepository,
        IPlaceholderUserRepository _placeholderUserRepository,
        IUnitOfWork _unitOfWork)
    {
        public async Task<List<Student>> GetAllStudent()
        {
            return await _studentRepository.GetAll();
        }

        public async Task<Student?> ImportFromPlaceholder(int idPlaceholderUser)
        {
            var user = await _placeholderUserRepository.GetById(idPlaceholderUser);

            if (user == null)
            {
                return null;
            }

            var names = user.Name.Split(' ');

            var student = new Student
            {
                FirstMidName = string.Join(' ', names.SkipLast(1)),
                LastName = names.Last(),
                EnrollmentDate = DateTime.Now,
            };

            await _studentRepository.Add(student);
            await _unitOfWork.SaveChanges();
            return student;
        }
    }
}

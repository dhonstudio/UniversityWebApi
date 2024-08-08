using Application.Repositories;
using Domain.DTO;
using Domain.Entities;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class StudentFeature(IRepository<Student> _studentRepository,
        IPlaceholderUserRepository _placeholderUserRepository,
        IUnitOfWork _unitOfWork,
        ISieveProcessor _sieveProcessor)
    {
        public async Task<List<Student>> GetAllStudent(SieveModel sieveModel)
        {
            var result = _studentRepository.GetAllSieveModel();
            var resultSieve = _sieveProcessor.Apply(sieveModel, result);

            return resultSieve.ToList();
        }

        public async Task<List<Student>?> GetFromStudent()
        {
            var student = await _placeholderUserRepository.GetFromStudent();

            if (student == null)
            {
                return null;
            }
            return student;
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

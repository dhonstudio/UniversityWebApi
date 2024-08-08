using Application.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class Repository<T>(SchoolContext _schoolContext) : IRepository<T> where T : BaseEntity
    {
        public async Task Add(T entity)
        {
            await _schoolContext
                .Set<T>()
                .AddAsync(entity);
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _schoolContext
                .Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _schoolContext
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _schoolContext
                .Set<T>()
                .AsNoTracking();
        }

        public async Task<T?> GetById(object id)
        {
            return await _schoolContext
                .Set<T>()
                .FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _schoolContext
                .Set<T>()
                .Remove(entity);
        }

        public async Task Update(T entity)
        {
            _schoolContext
                .Set<T>()
                .Update(entity);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Common;
using Test.DAL.Context;
using Test.DAL.Repositories.Interfaces;

namespace Test.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            Table.Update(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? expressionOrderBy = null, bool isDescending = false)
        {
            IQueryable<T> query = Table.Where(x => !x.IsDeleted);
            if(expressionOrderBy != null)
            {
                query = isDescending ? query.OrderByDescending(expressionOrderBy) : query.OrderBy(expressionOrderBy);
            }
            if(expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

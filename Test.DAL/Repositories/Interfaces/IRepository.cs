using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Common;

namespace Test.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity
    {
        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null,
                                          Expression<Func<T, object>>? expressionOrderBy = null,
                                          bool isDescending=false);
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public Task<T> Update(T entity);
        public Task Delete(T entity);
        public Task SaveChangesAsync();
        DbSet<T> Table { get; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Business.ViewModels.BlogVMs;
using Test.Core.Common;
using Test.Core.Entities;

namespace Test.Business.Services.Interfaces
{
    public interface IBlogService
    {
        public Task<IQueryable<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? expression = null,
                                  Expression<Func<Blog, object>>? expressionOrderBy = null,
                                  bool isDescending = false);
        public Task<Blog> GetByIdAsync(int id);
        public Task<Blog> CreateAsync(CreateBlogVm blogVm, string env);
        public Task<Blog> Update(UpdateBlogVm blogVm, string env);
        public Task Delete(int id);
    }
}

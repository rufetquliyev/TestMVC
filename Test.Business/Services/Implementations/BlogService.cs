using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Exceptions.Blog;
using Test.Business.Helpers;
using Test.Business.Services.Interfaces;
using Test.Business.ViewModels.BlogVMs;
using Test.Core.Entities;
using Test.DAL.Repositories.Interfaces;

namespace Test.Business.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<Blog> CreateAsync(CreateBlogVm blogVm, string env)
        {
            Blog blog = _mapper.Map<Blog>(blogVm);
            if (blogVm.Image != null)
            {
                if (blogVm.Image.CheckContent("image/"))
                {
                    if (blogVm.Image.CheckLength(2097152))
                    {
                        blog.ImgUrl = blogVm.Image.UploadFile(env, @"/Upload/BlogImages/");
                        await _blogRepository.CreateAsync(blog);
                        await _blogRepository.SaveChangesAsync();
                    }
                }
            }
            return blog;
        }

        public async Task Delete(int id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) throw new BLogNotFoundException();
            await _blogRepository.Delete(blog);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? expression = null, Expression<Func<Blog, object>>? expressionOrderBy = null, bool isDescending = false)
        {
            return await _blogRepository.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) throw new BLogNotFoundException();
            return blog;
        }

        public async Task<Blog> Update(UpdateBlogVm blogVm, string env)
        {
            Blog blog = await _blogRepository.GetByIdAsync(blogVm.Id);
            _mapper.Map(blog, blogVm);
            if (blogVm.Image != null)
            {
                if (blogVm.Image.CheckContent("image/"))
                {
                    if (blogVm.Image.CheckLength(2097152))
                    {
                        blog.ImgUrl.DeleteFile(env, @"/Upload/BlogImages/");
                        blog.ImgUrl = blogVm.Image.UploadFile(env, @"/Upload/BlogImages/");
                    }
                }
            }
            await _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
            return blog;
        }
    }
}

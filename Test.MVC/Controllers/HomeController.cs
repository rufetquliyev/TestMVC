using Microsoft.AspNetCore.Mvc;
using Test.Business.Services.Interfaces;
using Test.Business.ViewModels.UserVMs;
using Test.Core.Entities;

namespace Test.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Blog> blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }
    }
}

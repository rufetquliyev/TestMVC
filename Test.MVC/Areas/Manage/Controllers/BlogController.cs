using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Business.Services.Interfaces;
using Test.Business.ViewModels.BlogVMs;
using Test.Business.ViewModels.UserVMs;

namespace Test.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BlogController(IBlogService service, IWebHostEnvironment env, IMapper mapper)
        {
            _service = service;
            _env = env;
            _mapper = mapper;
        }
        [Authorize("Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            var blogs = await _service.GetAllAsync();
            return View(blogs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize("Admin")]

        public async Task<IActionResult> Create(CreateBlogVm blogVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var blog = await _service.CreateAsync(blogVm, _env.WebRootPath);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize("Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var blog = await _service.GetByIdAsync(id);
            UpdateBlogVm blogVm = _mapper.Map<UpdateBlogVm>(blog);
            return View(blogVm);
        }
        [HttpPost]
        [Authorize("Admin")]

        public async Task<IActionResult> Update(UpdateBlogVm blogVm)
        {
            await _service.Update(blogVm, _env.WebRootPath);
            return RedirectToAction("Index");
        }
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

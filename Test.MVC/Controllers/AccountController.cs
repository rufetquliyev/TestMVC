using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Test.Business.Services.Interfaces;
using Test.Business.ViewModels.UserVMs;
using Test.Core.Entities;

namespace Test.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVm registerVm)
        {
            var validationResult = new UserRegisterValidator();
            var result = validationResult.Validate(registerVm);
            foreach (var failure in result.Errors)
            {
                ModelState.AddModelError("", errorMessage: failure.ErrorMessage);
            }
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            await _accountService.Register(registerVm);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm loginVm)
        {
            var validationResult = new UserLoginValidator();
            var result = validationResult.Validate(new UserLoginVm());
            foreach (var failure in result.Errors)
            {
                ModelState.AddModelError("", errorMessage: failure.ErrorMessage);
            }
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            User user = await _accountService.Login(loginVm);

            if (user == null)
            {
                ModelState.AddModelError("Password", "Username or Password is wrong");
                return View(loginVm);
            }

            var res = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);

            if (!res.Succeeded)
            {
                ModelState.AddModelError("Password", "Username or Password is wrong");
                return View(loginVm);
            }

            if (res.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked");
                return View(loginVm);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRole();
            return RedirectToAction("Index", "Home");
        }
    }
}

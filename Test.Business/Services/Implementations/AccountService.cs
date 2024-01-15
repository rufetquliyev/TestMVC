using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Exceptions.User;
using Test.Business.Helpers;
using Test.Business.Services.Interfaces;
using Test.Business.ViewModels.UserVMs;
using Test.Core.Entities;

namespace Test.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<User> Login(UserLoginVm loginVm)
        {
            var user = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail) ?? await _userManager.FindByNameAsync(loginVm.UsernameOrEmail);
            if (user == null) throw new UserNotFoundException();
            return user;
        }

        public async Task Register(UserRegisterVm registerVm)
        {
            User appUser = _mapper.Map<User>(registerVm);
            await _userManager.CreateAsync(appUser, registerVm.Password);
            await _userManager.AddToRoleAsync(appUser, "Admin");
        }
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                }
            }
        }
    }
}

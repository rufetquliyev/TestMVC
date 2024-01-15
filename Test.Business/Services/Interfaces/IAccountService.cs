using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.ViewModels.UserVMs;
using Test.Core.Entities;

namespace Test.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task Register(UserRegisterVm registerVm);
        public Task<User> Login(UserLoginVm loginVm);
        public Task CreateRole();
    }
}

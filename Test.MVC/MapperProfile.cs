using AutoMapper;
using Test.Business.ViewModels.BlogVMs;
using Test.Business.ViewModels.UserVMs;
using Test.Core.Entities;

namespace Test.MVC
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateBlogVm, Blog>().ReverseMap();
            CreateMap<UpdateBlogVm, Blog>().ReverseMap();
            CreateMap<UserLoginVm, User>().ReverseMap();
            CreateMap<UserRegisterVm, User>().ReverseMap();
        }
    }
}

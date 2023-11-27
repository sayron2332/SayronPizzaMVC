using AutoMapper;
using SayronPizzaMVC.Core.DTO_s;
using SayronPizzaMVC.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.AutoMappers.User
{
    internal class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<LoginUserDto, AppUser>().ReverseMap();
            CreateMap<UsersDto, AppUser>().ReverseMap();
            CreateMap<CreateUserDto, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<AppUser, EditUserDto>().ReverseMap();
        }
     
    }
}

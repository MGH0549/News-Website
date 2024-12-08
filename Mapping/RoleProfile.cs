using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewsWebsite.Models;
using System.Web.Providers.Entities;
namespace NewsWebsite.Mapping
   
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
            CreateMap<IdentityUser, ManageUserinRole>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
           .ReverseMap();
        }
        
    }
}

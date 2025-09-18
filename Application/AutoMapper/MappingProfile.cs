using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<TaskEntity, TaskModel>().ForMember(dest => dest.User, src => src.MapFrom(t => t.User!.Name)).ReverseMap();
        }
    }
}

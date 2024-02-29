using AutoMapper;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Models;

namespace UserApi_Identity.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}
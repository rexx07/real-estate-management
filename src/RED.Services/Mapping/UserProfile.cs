using AutoMapper;
using Core.Domain.Users;
using RED.Services.Dtos;

namespace RED.Services.Mapping;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
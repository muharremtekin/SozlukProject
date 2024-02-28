using AutoMapper;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Common.Models.Queries;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();
    }
}


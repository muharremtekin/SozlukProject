using AutoMapper;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Common.Models.Queries;

namespace SoclukProject.Api.Application.Interfaces.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
    }
}


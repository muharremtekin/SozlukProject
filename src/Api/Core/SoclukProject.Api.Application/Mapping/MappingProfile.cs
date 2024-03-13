using AutoMapper;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Common.Models.RequestModels;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
        CreateMap<User, UserDetailViewmodel>();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();

        CreateMap<CreateEntryCommand, Entry>()
            .ReverseMap();

        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));

        CreateMap<CreateEntryCommentCommand, EntryComment>()
            .ReverseMap();
    }
}


using AutoMapper;
using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetUserDetail;
public class GetUserDetailQueryHanler : IRequestHandler<GetUserDetailQuery, UserDetailViewmodel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDetailQueryHanler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailViewmodel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        Domain.Models.User dbUser = null;

        if (request.UserId != Guid.Empty)
            dbUser = await _userRepository.GetByIdAsync(request.UserId);
        else if (!string.IsNullOrEmpty(request.UserName))
            dbUser = await _userRepository.GetSingleAsync(u => u.UserName == request.UserName);

        // TODO: ikisi de  boş gelirse hata fırlat!

        return _mapper.Map<UserDetailViewmodel>(dbUser);

    }
}


﻿using MediatR;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetUserDetail;
public class GetUserDetailQuery : IRequest<UserDetailViewmodel>
{
    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; }
}


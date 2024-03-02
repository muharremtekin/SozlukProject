﻿using MediatR;

namespace SoclukProject.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommand : IRequest<bool>
{
    public CreateEntryFavCommand(Guid? entryId, Guid? userId)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid? EntryId { get; set; }
    public Guid? UserId { get; set; }
}


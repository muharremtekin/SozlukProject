﻿using MediatR;

namespace SoclukProject.Common.Events.User;

public class ChangeUserPasswordCommand : IRequest<bool>
{
    public ChangeUserPasswordCommand(Guid? userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }

    public Guid? UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}


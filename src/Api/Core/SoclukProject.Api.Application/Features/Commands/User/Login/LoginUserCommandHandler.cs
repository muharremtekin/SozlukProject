using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Infrastructure.Exceptions;
using SoclukProject.Common.Models.RequestModels;
using SoclukProject.Common.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoclukProject.Api.Application.Features.Commands.User.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configration = configration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbuser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (dbuser == null)
            throw new DatabaseValidationException("User not found!");

        var pass = PasswordEncryptor.Encrypt(request.Password);

        if (dbuser.Password != pass)
            throw new DatabaseValidationException("Password is wrong!");

        if (!dbuser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is not confirmed yet!");

        var result = _mapper.Map<LoginUserViewModel>(dbuser);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, dbuser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbuser.EmailAddress),
            new Claim(ClaimTypes.Name, dbuser.UserName),
            new Claim(ClaimTypes.GivenName, dbuser.FirstName),
            new Claim(ClaimTypes.Surname, dbuser.LastName),
        };

        result.Token = GenerateToken(claims);

        return result;

    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["AuthConfig:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expire = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: creds, notBefore: DateTime.UtcNow);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


using MediatR;

namespace SoclukProject.Common.Models.RequestModels;
public class UpdateUserCommand : IRequest<Guid> // normalde bir şey döndürmeye gerek yok.
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
}


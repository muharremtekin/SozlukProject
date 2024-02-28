using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}


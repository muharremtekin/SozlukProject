using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}


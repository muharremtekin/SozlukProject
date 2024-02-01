using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EmailConfirmationRepository : EntityRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(DbContext dbContext) : base(dbContext)
    {
    }
}


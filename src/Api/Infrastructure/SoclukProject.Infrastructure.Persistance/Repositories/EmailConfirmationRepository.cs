using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EmailConfirmationRepository : EntityRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}


using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EntryRepository : EntityRepository<Entry>, IEntryRepository
{
    public EntryRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}


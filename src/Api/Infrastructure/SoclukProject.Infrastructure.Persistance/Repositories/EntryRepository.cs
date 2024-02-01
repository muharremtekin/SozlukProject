using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EntryRepository : EntityRepository<Entry>, IEntryRepository
{
    public EntryRepository(DbContext dbContext) : base(dbContext)
    {
    }
}


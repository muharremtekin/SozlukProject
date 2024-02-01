using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EntryCommentRepository : EntityRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(DbContext dbContext) : base(dbContext)
    {
    }
}


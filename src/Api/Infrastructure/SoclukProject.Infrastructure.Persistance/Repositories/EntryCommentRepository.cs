using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EntryCommentRepository : EntityRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}


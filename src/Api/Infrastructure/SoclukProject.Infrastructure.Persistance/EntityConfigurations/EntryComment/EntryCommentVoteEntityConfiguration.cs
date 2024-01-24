using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.EntityConfigurations.EntryComment;
public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentvote", SozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(e => e.EntryComment)
            .WithMany(e => e.EntryCommentVotes)
            .HasForeignKey(e => e.EntryCommentId);
    }
}


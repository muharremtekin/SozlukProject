using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;

namespace SoclukProject.Infrastructure.Persistance.EntityConfigurations.EntryComment;
public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentfavorite", SozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(e => e.EntryComment)
            .WithMany(e => e.EntryCommentFavorites)
            .HasForeignKey(e => e.EntryCommentId);

        builder.HasOne(e => e.CreatedUser)
            .WithMany(e => e.EntryCommentFavorites)
            .HasForeignKey(e => e.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Domain.Models;
using System.Reflection;

namespace SoclukProject.Infrastructure.Persistance.Context
{
    public class SozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public SozlukContext()
        {

        }
        public SozlukContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<EntryFavorite> EntryFavorites { get; set; }
        DbSet<EntryVote> EntryVotes { get; set; }
        DbSet<EntryComment> EntryComments { get; set; }
        DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
        DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
        DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost;Database=SozlukProject;Port=5432;User ID=postgres;Password=mysecretpassword";
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                    .Where(e => e.State == EntityState.Added)
                                    .Select(e => (BaseEntity)e.Entity);
            PrepareAddedEntities(addedEntities);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedAt == DateTime.MinValue)
                    entity.CreatedAt = DateTime.Now;
            }

        }
    }
}

using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Infrastructure.Persistance.Context;
using System.Linq.Expressions;

namespace SoclukProject.Infrastructure.Persistance.Repositories;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    private readonly SozlukContext dbContext;
    protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

    public EntityRepository(SozlukContext dbContext) =>
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    #region Add methods
    public int Add(TEntity entity)
    {
        this.entity.Add(entity);
        return dbContext.SaveChanges();
    }

    public int Add(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;

        entity.AddRange(entity);
        return dbContext.SaveChanges();
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;

        await entity.AddRangeAsync(entities);
        return await dbContext.SaveChangesAsync();
    }
    #endregion

    #region Update region
    public int Update(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return dbContext.SaveChanges();
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return await dbContext.SaveChangesAsync();
    }

    #endregion

    #region AddOrUpdate Methods

    public int AddOrUpdate(TEntity entity)
    {
        if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            dbContext.Update(entity);

        return dbContext.SaveChanges();
    }

    public async Task<int> AddOrUpdateAsync(TEntity entity)
    {
        // check the entity with the id already tracked
        if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            dbContext.Update(entity);

        return await dbContext.SaveChangesAsync();
    }
    #endregion

    #region bulk methods
    public async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            await Task.CompletedTask;

        await entity.AddRangeAsync(entities);

        await dbContext.SaveChangesAsync();
    }

    public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChangesAsync();
    }

    public Task BulkDelete(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        entity.RemoveRange(entities);
        return dbContext.SaveChangesAsync();
    }

    public Task BulkDeleteById(IEnumerable<Guid> ids)
    {
        if (ids != null && !ids.Any())
            return Task.CompletedTask;

        dbContext.RemoveRange(entity.Where(i => ids.Contains(i.Id)));
        return dbContext.SaveChangesAsync();
    }

    public Task BulkUpdate(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        foreach (var entityItem in entities)
        {
            entity.Update(entityItem);
        }

        return dbContext.SaveChangesAsync();
    }
    #endregion

    #region delete methods
    public int Delete(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
            this.entity.Attach(entity);

        this.entity.Remove(entity);

        return dbContext.SaveChanges();
    }

    public int Delete(Guid id)
    {
        var entity = this.entity.Find(id);
        return Delete(entity);
    }

    public async Task<int> DeleteAsync(Guid entityId)
    {
        var entity = await this.entity.FindAsync(entityId);
        return await DeleteAsync(entity);
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);

        return await dbContext.SaveChangesAsync();
    }

    public bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChanges() > 0;
    }
    #endregion

    #region SaveChanges Methods

    public Task<int> SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    #endregion

    #region Get Methods
    public IQueryable<TEntity> AsQueryable() => entity.AsQueryable();
    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        return Get(predicate, noTracking, includes).FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
    {
        if (noTracking)
            return await entity.AsNoTracking().ToListAsync();

        return await entity.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity found = await entity.FindAsync(id);

        if (found == null)
            return null;

        if (noTracking)
            dbContext.Entry(found).State = EntityState.Detached;

        foreach (Expression<Func<TEntity, object>> include in includes)
            dbContext.Entry(found).Reference(include).Load();

        return found;
    }

    public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (noTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync();
    }

    #endregion

    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            foreach (var includeItem in includes)
                query = query.Include(includeItem);
        }

        return query;
    }
}


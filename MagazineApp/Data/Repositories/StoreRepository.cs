using MagazineApp.Data.Contexts;
using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MagazineApp.Data.Repositories;
public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext dbContext;
    protected readonly DbSet<Store> dbSet;
    public StoreRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<Store>();
    }
    public async ValueTask<bool> DeleteAsync(Expression<Func<Store, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }

    public async ValueTask<Store> InsertAsync(Store entity)
    => (await this.dbSet.AddAsync(entity)).Entity;

    public async ValueTask SaveAsync()
        => await dbContext.SaveChangesAsync();

    public IQueryable<Store> SelectAll(Expression<Func<Store, bool>> expression = null, string[] includes = null)
    {
        IQueryable<Store> query = dbContext.Stores;

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public async ValueTask<Store> SelectAsync(Expression<Func<Store, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

    public Store Update(Store entity)
        => (this.dbContext.Update(entity)).Entity;
}

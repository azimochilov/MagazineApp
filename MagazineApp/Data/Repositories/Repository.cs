using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Commons;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using MagazineApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MagazineApp.Data.Repositories;
public class Repository<T> : IRepository<T> where T : Auditable
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<T> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }

    public async ValueTask<T> InsertAsync(T entity)
    => (await this.dbSet.AddAsync(entity)).Entity;

    public async ValueTask SaveAsync()
        => await dbContext.SaveChangesAsync();

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

    public T Update(T entity)
        => (this.dbContext.Update(entity)).Entity;
}

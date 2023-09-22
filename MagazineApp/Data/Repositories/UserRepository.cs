using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MagazineApp.Data.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagazineApp.Data.Repositories;
public class UserRepository :IUserRepository
{
    private readonly AppDbContext dbContext;
    protected readonly DbSet<User> dbSet;
    public UserRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<User>();
    }
    public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }

    public async ValueTask<User> InsertAsync(User entity)
    => (await this.dbSet.AddAsync(entity)).Entity;

    public async ValueTask SaveAsync()
        => await dbContext.SaveChangesAsync();

    public IQueryable<User> SelectAll(Expression<Func<User, bool>> expression = null, string[] includes = null)
    {
        IQueryable<User> query = dbContext.Users;

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

    public async ValueTask<User> SelectAsync(Expression<Func<User, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

    public User Update(User entity)
        => (this.dbContext.Update(entity)).Entity;
}

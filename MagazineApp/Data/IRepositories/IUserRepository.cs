using MagazineApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace MagazineApp.Data.IRepositories;
public interface IUserRepository
{
    ValueTask<User> InsertAsync(User entity);
    User Update(User entity);
    IQueryable<User> SelectAll(Expression<Func<User, bool>> expression = null, string[] includes = null);
    ValueTask<User> SelectAsync(Expression<Func<User, bool>> expression, string[] includes = null);
    ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    ValueTask SaveAsync();
}

using MagazineApp.Domain.Entities;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MagazineApp.Data.IRepositories;
public interface IStoreRepository
{
    ValueTask<Store> InsertAsync(Store entity);
    Store Update(Store entity);
    IQueryable<Store> SelectAll(Expression<Func<Store, bool>> expression = null, string[] includes = null);
    ValueTask<Store> SelectAsync(Expression<Func<Store, bool>> expression, string[] includes = null);
    ValueTask<bool> DeleteAsync(Expression<Func<Store, bool>> expression);
    ValueTask SaveAsync();
}

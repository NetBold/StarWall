using System.Linq.Expressions;
using StarWall.Core.Models;
using X.PagedList;

namespace StarWall.Core.Interfaces;

public interface IGenericRepository<T> where T: class
{
    Task Insert(T entity);
    void Update(T entity);
    Task Delete(long id);
    Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
    Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string> includes = null);
}
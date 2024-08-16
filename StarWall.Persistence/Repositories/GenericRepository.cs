using Microsoft.EntityFrameworkCore;
using StarWall.Core.Interfaces;
using StarWall.Core.Models;
using StarWall.Persistence.DataBaseContext;
using System.Linq.Expressions;
using X.PagedList;

namespace StarWall.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _db;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public async Task Delete(long id)
    {
        var entity = await _db.FindAsync(id);
        _db.Remove(entity);
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
    {
        IQueryable<T> query = _db;
        if (includes != null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, List<string> includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
        IQueryable<T> query = _db;

        if (expression != null)
            query = query.Where(expression);

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        if (orderBy != null)
            query = orderBy(query);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string> includes = null)
    {
        IQueryable<T> query = _db;

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
    }

    public async Task Insert(T entity)
    {
        await _db.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _db.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
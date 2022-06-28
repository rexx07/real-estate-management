using System.Linq.Expressions;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using RED.Services.ServiceBase.Interfaces;

namespace RED.Services.ServiceBase.Services;

public abstract class ServiceBase<T> : IServiceBase<T> where T : class
{
    protected RedDbContext _context { get; set; }
    public ServiceBase(RedDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
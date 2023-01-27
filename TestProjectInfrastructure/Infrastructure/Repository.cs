using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity GetByID(int ID);
    Task<TEntity> GetByIDAsync(int ID);
    void AddPart(TEntity model);
    void AddParts(IEnumerable<TEntity> model);
    void DeletePart(TEntity model);
    void DeleteParts(IEnumerable<TEntity> model);
    void ClearTable();
}
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal CometDBContext _context;
    internal DbSet<TEntity> _dbSet;
    public Repository(CometDBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    public virtual TEntity GetByID(int ID)
    {
        return _dbSet.Find(ID);
    }

    public virtual async Task<TEntity> GetByIDAsync(int ID)
    {
        return await _dbSet.FindAsync(ID);
    }

    public virtual void AddPart(TEntity model)
    {
        _context.Entry(model).State = EntityState.Added;
    }
    public virtual void AddParts(IEnumerable<TEntity> models)
    {
        foreach (var model in models)
        {
            _context.Entry(model).State = EntityState.Added;
        }
    }

    public virtual void DeletePart(TEntity model)
    {
        _context.Entry(model).State = EntityState.Deleted;
    }

    public virtual void DeleteParts(IEnumerable<TEntity> models)
    {
        foreach (var model in models)
        {
            _context.Entry(model).State = EntityState.Deleted;
        }
    }
    public void ClearTable()
    {
        var entityType = _context.Model.FindEntityType(typeof(TEntity));
        if (entityType == null)
        {
            throw new InvalidOperationException("Repository.ClearTable FindEntityType: can't fined FindEntityType");
        }
        _context.Database.ExecuteSqlRaw(
            $"TRUNCATE TABLE  {entityType.GetSchema()}.{entityType.GetTableName()}");
    }
}

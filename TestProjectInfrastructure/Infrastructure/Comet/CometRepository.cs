using System;
using System.Linq.Expressions;
using TestProject.Infrastructure.Models;

namespace TestProject.Infrastructure;

public interface ICometRepository : IRepository<Comet>
{
    IQueryable<Comet> Find(Expression<Func<Comet, bool>> predicate);
    List<int> GetYears();
}
public class CometRepository : Repository<Comet>, ICometRepository
{
    public CometRepository(CometDBContext context) : base(context)
    {

    }
    public IQueryable<Comet> Find(Expression<Func<Comet, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    public List<int> GetYears()
    {
        return _dbSet.Where(x=>x.Year.HasValue).GroupBy(x=>x.Year.Value.Year).Select(x=>x.Key).OrderBy(x=>x).ToList();
    }
}


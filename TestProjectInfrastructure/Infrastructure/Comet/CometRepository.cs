using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestProject.Infrastructure.Models;

namespace TestProject.Infrastructure;

public interface ICometRepository : IRepository<Comet>
{
    IQueryable<Comet> Find(Expression<Func<Comet, bool>> predicate);
}
public class CometRepository : Repository<Comet>, ICometRepository
{
    public CometRepository(CometDBContext context) : base(context)
    {

    }
    public virtual IQueryable<Comet> Find(Expression<Func<Comet, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
}


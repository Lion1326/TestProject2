using Microsoft.EntityFrameworkCore;
using TestProject.Infrastructure.Models;

namespace TestProject.Infrastructure;

public interface IRecclassRepository : IRepository<Recclass>
{
    Recclass GetByName(string name);
    Task<Recclass> GetByNameAsync(string name);
    IQueryable<Recclass> GetList();
}
public class RecclassRepository : Repository<Recclass>, IRecclassRepository
{
    public RecclassRepository(CometDBContext context) : base(context)
    {

    }
    public Recclass GetByName(string name)
    {
        return _dbSet.First(x => x.Name == name);
    }
    public async Task<Recclass> GetByNameAsync(string name)
    {
        return await _dbSet.FirstAsync(x => x.Name == name);
    }
    public IQueryable<Recclass> GetList()
    {
        return _dbSet;
    }
}

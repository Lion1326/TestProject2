using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TestProject.Infrastructure.Models;
using TestProject.Helpers;

namespace TestProject.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    ICometRepository cometRepository { get; }
    IRecclassRepository recclassRepository { get; }
    void Save();
    Task SaveAsync();
    void SaveChangesWithIdentityInsert<T>();
    void BeginTransaction();
    Task BeginTransactionAsync();
    void CommitTransaction();
    Task CommitTransactionAsync();
    void RollbackTransaction();
    Task RollbackTransactionAsync();
}
public class UnitOfWork : IUnitOfWork
{
    protected CometDBContext _context;
    public UnitOfWork(CometDBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #region Repositories 
    ICometRepository _cometRepository;
    public virtual ICometRepository cometRepository
    {
        get
        {
            if (_cometRepository == null) _cometRepository = new CometRepository(_context);
            return _cometRepository;
        }
    }
    IRecclassRepository _recclassRepository;
    public virtual IRecclassRepository recclassRepository
    {
        get
        {
            if (_recclassRepository == null) _recclassRepository = new RecclassRepository(_context);
            return _recclassRepository;
        }
    }
    #endregion

    public virtual void Save()
    {
        _context.SaveChanges();
    }

    public virtual async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void SaveChangesWithIdentityInsert<T>()
    {
        _context.EnableIdentityInsert<T>();
        _context.SaveChanges();
        _context.DisableIdentityInsert<T>();
    }
    private IDbContextTransaction _transaction { get; set; }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }
    public void CommitTransaction()
    {
        if (_transaction != null)
            _transaction.Commit();
    }
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }
    public void RollbackTransaction()
    {
        if (_transaction != null)
            _transaction.Rollback();
    }
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
                _context.Dispose();
            }
        }
        disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
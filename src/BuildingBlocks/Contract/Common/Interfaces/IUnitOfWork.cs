using Microsoft.EntityFrameworkCore;


namespace Contract.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
    }
}

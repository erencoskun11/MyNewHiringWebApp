using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<IEnumerable<TEntity>> ListAsync(CancellationToken ct = default);

        // predicate overload
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        // find single by predicate
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}

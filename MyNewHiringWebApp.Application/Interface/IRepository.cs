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
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        // Pagination with optional predicate
        Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}

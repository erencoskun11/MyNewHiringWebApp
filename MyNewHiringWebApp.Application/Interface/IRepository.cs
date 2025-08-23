using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity);

        // Pagination
        Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);
    }
}

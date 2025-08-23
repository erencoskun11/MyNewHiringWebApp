using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbSet = _db.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, ct);
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default)
        {
            return await _dbSet.ToListAsync(ct);
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(ct);
        }

        public virtual async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = predicate == null ? _dbSet.AsQueryable() : _dbSet.Where(predicate);

            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            return (items, totalCount);
        }
    }
}



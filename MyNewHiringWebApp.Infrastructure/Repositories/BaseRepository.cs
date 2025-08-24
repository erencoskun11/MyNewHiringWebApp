using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbSet = _db.Set<TEntity>();
        }

        // Add / Update / Remove
        public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
            => await _dbSet.AddAsync(entity, ct);

        public virtual void Update(TEntity entity) => _dbSet.Update(entity);

        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);

        // Get by id
        public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _dbSet.FindAsync(new object[] { id }, ct);

        // Find
        public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => await _dbSet.FirstOrDefaultAsync(predicate, ct);

        // List
        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default)
            => await _dbSet.ToListAsync(ct);

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => await _dbSet.Where(predicate).ToListAsync(ct);

        // Paged list
        public virtual async Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = predicate == null ? _dbSet.AsQueryable() : _dbSet.Where(predicate);

            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            return (items, totalCount);
        }

        // Save changes
        public virtual async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _db.SaveChangesAsync(ct);
    }
}

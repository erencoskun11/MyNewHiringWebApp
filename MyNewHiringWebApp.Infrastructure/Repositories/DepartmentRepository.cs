using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Department?> GetByNameAsync(string name, CancellationToken ct = default)=>
        
            await _dbSet.FirstOrDefaultAsync(d => d.Name == name, ct);
        
       
    }
}

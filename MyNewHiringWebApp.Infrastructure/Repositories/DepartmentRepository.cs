using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Department?> GetByNameAsync(string name, CancellationToken ct = default)=>
        
            await _dbSet.FirstOrDefaultAsync(d => d.Name == name, ct);
        
       
    }
}

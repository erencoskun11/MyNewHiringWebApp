using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Skill?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Name == name, ct);
        }
    }
}

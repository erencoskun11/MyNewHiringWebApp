using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class InterviewRepository : BaseRepository<Interview> ,IInterviewRepository
    {
        public InterviewRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Interview?> GetWithRelationsAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet
               .Include(i => i.JobApplication)         
               .Include(i => i.Interviewer)        
               .FirstOrDefaultAsync(i => i.Id == id, ct);
        }
    }
}

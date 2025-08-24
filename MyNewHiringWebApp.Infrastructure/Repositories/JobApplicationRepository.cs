using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class JobApplicationRepository : BaseRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<JobApplication> GetWithDetailsAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet
       .Include(j => j.Candidate)
       .Include(j => j.JobPosition)
       .FirstOrDefaultAsync(j => j.Id == id, ct); 

        }
    }
}

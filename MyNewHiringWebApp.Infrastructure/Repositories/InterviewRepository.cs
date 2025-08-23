using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

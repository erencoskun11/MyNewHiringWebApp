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
    public class SubmittedAnswerRepository : BaseRepository<SubmittedAnswer>,ISubmittedAnswerRepository
    {
        public SubmittedAnswerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<SubmittedAnswer>> GetBySubmissionIdAsync(int submissionId, CancellationToken ct = default)
        {
            return await _dbSet.Where(c => c.TestSubmissionId==submissionId).Include(c => c.TestSubmission).ToListAsync(ct);
        }
    }
}
